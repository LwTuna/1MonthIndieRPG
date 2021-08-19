using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Slime : MonoBehaviour
{

    private Transform player;

    public float damageToPlayer; 

    public float aggroRange;

    private const float cooldown = 3f;

    private float currentAttackCooldown = 0f;
    private float attackPower = 12f;
    
    
    private enum EnemyState
    {
        Attacking,Idle
    }

    private EnemyState _state = EnemyState.Idle;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        _state = Math.Abs(Vector2.Distance(player.position, transform.position)) <= aggroRange ? EnemyState.Attacking : EnemyState.Idle;

        currentAttackCooldown -= Time.deltaTime;
        
        if (_state == EnemyState.Attacking && currentAttackCooldown <= 0f)
        {
            Attack();
            currentAttackCooldown = cooldown;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            other.gameObject.GetComponent<PlayerController>().Hurt(damageToPlayer,transform);
        }
    }

    private void Attack()
    {
        Vector2 normal = (player.position-transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(normal * attackPower,ForceMode2D.Impulse);
        

    }
}
