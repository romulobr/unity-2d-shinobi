using System;
using UnityEngine;
using System.Collections;

public class ShinobiController : MonoBehaviour
{
    public float Speed = 0.5f;
    public float JumpSpeed = 1.0f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    public float JumpDuration = 1000f;
    private bool isGrounded = false;
    private float totalJumpForce = 0.0f;

    public void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
    }



    private void Walk()
    {
        if (isGrounded && !animator.GetCurrentAnimatorStateInfo(0).IsName("sword-attack"))
        {
            animator.Play("walking");
            animator.SetBool("isWalking", true);
        }
    }

    private void Idle()
    {
        if (isGrounded)
        {
            animator.Play("idle");
            animator.SetBool("isWalking", false);
        }
    }

    private void HandleMovement()
    {
        var horizontalAxis = getRoundedBinaryHorizontal();
        if (Math.Abs(horizontalAxis) > 0.1)
        {
            rigidBody2D.velocity = new Vector2(horizontalAxis*Speed, rigidBody2D.velocity.y);
            spriteRenderer.flipX = horizontalAxis < 0;
            Walk();
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("sword-attack"))
            {
                Idle();
            }
        }
    }

    private void HandleJumping()
    {
        if (ShouldStartJumping())
        {
            isGrounded = false;
            animator.Play("jumping");
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*JumpSpeed);
        }        
    }

    private bool ShouldStartJumping()
    {
        return Input.GetButton("Jump") && Input.GetAxis("Jump") > 0 && isGrounded;
    }

    private bool IsAttacking()
    {
        return Input.GetButtonDown("Fire1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("sword-attack") &&
               Input.GetAxis("Fire1") > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigidBody2D.velocity.y <= 0.1)
        {
            isGrounded = true;
            HandleMovement();
        }
    }

    public void Update()
    {
        if (IsAttacking())
        {
            animator.Play("sword-attack");
            animator.SetBool("isWalking", false);
        }
    }

    public void FixedUpdate()
    {
//        HandleMovement();
        HandleJumping();
    }
}