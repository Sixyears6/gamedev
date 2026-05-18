using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.Rendering.VirtualTexturing;

public class EnemyMovement : MonoBehaviour
{
    public float attackRange = 2;
    public float speed;
    public float attackCooldown = 2;
    public float playerDetectionRange = 5;
    public Transform dectectionPoint;
    public LayerMask playerLayer;

    private float attacktimer;
    private int facingDirec = -1;
    private EnemyState enemyState, newState;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        CheckForPlayer();
        if (attacktimer >0)
        {
            attacktimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        
        else if (enemyState == EnemyState.Attacking)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void Chase()
    {
        if (player.position.x > transform.position.x && facingDirec == -1 ||
            player.position.x < transform.position.x && facingDirec == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }


    void Flip()
    {
        facingDirec *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(dectectionPoint.position, playerDetectionRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attacktimer <= 0)
            {
                attacktimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else 
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        // Exit current state
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        //Update current state
        enemyState = newState;

        //update animation
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }   
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
}