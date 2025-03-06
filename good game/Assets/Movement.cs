using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.TextCore;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    private float a;

    private float facing = 1;
    private bool facingT = true;


    [SerializeField] private float speed;
    [SerializeField] private float hight;
    [SerializeField] private LayerMask jumpableGround;






    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }



    void Update()
    {

        Controller();

        Animation();

    }

    private void Controller()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        Move();
    }

    private void Animation()
    {

        bool jump = rb.velocity.y > 0;
        bool fall = rb.velocity.y < 0;
        bool ismoving = rb.velocity.x != 0;
        anim.SetBool("run", ismoving);
        anim.SetBool("jump", jump);
        anim.SetBool("fall", fall);
        anim.SetBool("ground", IsGrounded());

    }


    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, hight);
    }


    private void Move()
    {
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
        if (rb.velocity.x * facing < 0)
        {
            Flip();
        }
    }
    private void Flip()
    {
        sprite.flipX = facingT;
        facing *= -1;
        facingT = !facingT;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size,  0, Vector2.down, .1f, jumpableGround);
    }
}
