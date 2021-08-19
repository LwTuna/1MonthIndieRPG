using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InWorldItem : MonoBehaviour
{
    
    private const float startMoving = 2f;
    
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public static InWorldItem SpawnItemInWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemDatabase.Instance.prefabItem, position, Quaternion.identity).transform;
        InWorldItem inWorldItem = transform.GetComponent<InWorldItem>();
        inWorldItem.SetItem(item);
        return inWorldItem;
    }

    private static InWorldItem SpawnItemInWorld(Vector3 position, Item item,Vector2 force)
    {
        InWorldItem inWorldItem = SpawnItemInWorld(position, item);
        inWorldItem.GetComponent<Rigidbody2D>().AddForce(force,ForceMode2D.Impulse);
        return inWorldItem;
    }

    public static InWorldItem SpawnItemInWorld(Vector3 position, Item item,float force)
    {
        
        Vector2 f = new Vector2(Random.Range(-1f,1f) * force,Random.Range(-1f,1f) * force);
        InWorldItem inWorldItem = SpawnItemInWorld(position, item,f);
        return inWorldItem;
    }

    public Item item;
    private GameObject _player;

    private void SetItem(Item item)
    {
        this.item = item;
        GetComponent<SpriteRenderer>().sprite = item._sprite;
        
    }

    public void Update()
    {
        if (ShouldMoveToPlayer(_player))
        {
            GetComponent<Rigidbody2D>().MovePosition(Vector3.Lerp(transform.position,_player.transform.position,0.05f));
        }
    }


    private bool ShouldMoveToPlayer(GameObject player)
    {
        float distance =  Vector3.Distance(player.transform.position, transform.position);
        return distance <= startMoving;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() == null) return;
        other.gameObject.GetComponent<PlayerController>().PickUpItem(item);
        Destroy(gameObject);
    }
}
