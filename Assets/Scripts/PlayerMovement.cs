using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public int facingDirec = 1;

    public Rigidbody2D rb;
    public Animator anim;

    private void FixedUpdate() //Fixedupdate dc goi moi 50x frame
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0 && transform.localScale.x < 0 || horizontal < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        anim.SetFloat("horizontal",  Mathf.Abs(horizontal));
        anim.SetFloat("vertical", Mathf.Abs(vertical));

        rb.linearVelocity = new Vector2(horizontal, vertical) * speed;
    }
    void Flip()
    {
        facingDirec *= -1;
        transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
