using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemiesCombat : MonoBehaviour 
{
    public int damage = 1;
    public Transform attackpoint;
    public float weaponRange;
    public LayerMask playerLayer;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().changeHealth(damage);
        }
    }

    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackpoint.position, weaponRange, playerLayer);

        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().changeHealth(-damage);
        }
    }
}
