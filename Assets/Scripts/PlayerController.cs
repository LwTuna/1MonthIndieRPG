using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Camera _camera;
    private Rigidbody2D _rb;

    private Vector2 _movement;

    public float interactRange = 0.5f;

    public LayerMask interactLayer;

    public Transform interactionNorth, interactionSouth, interactionEast, interactionWest;


    public Sprite idleTop, idleLeft, idleRight, idleDown;
    
    public PlayerInventory _inventory;

    public UiManager uiManager;


    private Animator _animator;

    public float maxHealth = 10f;
    private float health;
    
   

    private Direction _direction;
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int Moving = Animator.StringToHash("moving");

    private Direction gizmoDirection;
    private void Awake()
    {
        
        _animator = GetComponent<Animator>();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
        _direction = Direction.South;
        _inventory = new PlayerInventory(uiManager);
        _inventory.SetHotBarItem(ItemDatabase.CreateItem(ItemDatabase.ItemType.WoodenPickaxe,1),0);
        health = maxHealth;

    }

    void Update()
    {
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        if (_movement.x > 0f) _direction = Direction.East;
        if (_movement.x < 0f) _direction = Direction.West;
        if (_movement.y < 0f) _direction = Direction.South;
        if (_movement.y > 0f) _direction = Direction.North;

        
        _animator.SetFloat(MoveX,_movement.x);
        _animator.SetFloat(MoveY,_movement.y);
        bool moving = Math.Abs(_movement.x) > 0.01f || Math.Abs(_movement.y) > 0.01f;
        _animator.SetBool(Moving, moving);
        if(moving) GetComponent<Animator>().enabled = true;
        
        CheckInteract();
        UpdateWhileHotbarSelected();
    }

    private void UpdateWhileHotbarSelected()
    {
        var currentItem = _inventory.GetCurrentItem();
        if (Camera.main != null)
            currentItem?.UpdateWhileSelected(this, Camera.main.ScreenToWorldPoint(Input.mousePosition),
                GetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition)), currentItem);
    }

    

    public void SetIdleSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _direction switch
        {
            Direction.North => idleTop,
            Direction.South => idleDown,
            Direction.East => idleRight,
            Direction.West => idleLeft,
            _ => idleTop
        };
        GetComponent<Animator>().enabled = false;
    }
    
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position+_movement * (movementSpeed * Time.fixedDeltaTime));
    }


    public void PickUpItem(Item item)
    {
        _inventory.AddItem(item);
    }

    private void CheckInteract()
    {
        if (!Input.GetMouseButton(0)) return;
        var currentItem = _inventory.GetCurrentItem();
        InteractWithoutItem();
        if (currentItem != null)
        {
            Vector2 mouseInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentItem.OnUse(this,mouseInWorld,GetDirection(mouseInWorld),currentItem,interactLayer);
            gizmoDirection = GetDirection(mouseInWorld);
        }
    }

    private Direction GetDirection(Vector2 mouseToWorld)
    {
        var position = transform.position;
        var distance = mouseToWorld - new Vector2(position.x,position.y);
        if (Math.Abs(distance.x) > Math.Abs(distance.y))
        {
            return distance.x > 0 ? Direction.East : Direction.West;
        }

        return distance.y > 0 ? Direction.North : Direction.South;
    }

    private void InteractWithoutItem()
    {
        
        var collisions = GetDirection(Camera.main.ScreenToWorldPoint(Input.mousePosition)) switch
        {
            
            Direction.North => Physics2D.OverlapCircleAll(interactionNorth.position, interactRange,interactLayer),
            Direction.South => Physics2D.OverlapCircleAll(interactionSouth.position, interactRange,interactLayer),
            Direction.East => Physics2D.OverlapCircleAll(interactionEast.position, interactRange,interactLayer),
            Direction.West => Physics2D.OverlapCircleAll(interactionWest.position, interactRange,interactLayer),
            _ => new Collider2D[0]
        };
        foreach (var collider in collisions)
        {
            var interactable = collider.GetComponent<Interactable>();
            interactable?.OnInteractWithoutItem(this);
            break;
        }
    }


    private void OnDrawGizmos()
    {
        switch (gizmoDirection)
        {
            case Direction.North:
                Gizmos.DrawSphere(interactionNorth.position,interactRange);
                break;
            case Direction.South:
                Gizmos.DrawSphere(interactionSouth.position,interactRange);
                break;
            case Direction.East:
                Gizmos.DrawSphere(interactionEast.position,interactRange);
                break;
            case Direction.West:
                Gizmos.DrawSphere(interactionWest.position,interactRange);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    

    public void Hurt(float damageToPlayer,Transform other)
    {
        
        health -= damageToPlayer;
        if (health <= 0)
        {
            Debug.Log("Dead");
        }
    }
}