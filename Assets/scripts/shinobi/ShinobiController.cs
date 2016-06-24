using System;
using UnityEngine;

namespace scripts.shinobi
{
    public class ShinobiController : MonoBehaviour
    {
        private readonly ShinobiMovementReader movementReader;
        private readonly ShinobiJumpReader jumpReader;
        public float Speed = 0.5f;
        public float JumpSpeed = 1.0f;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        public float JumpDuration = 1000f;
        private bool isGrounded = false;

        public ShinobiController()
        {
            this.jumpReader = new ShinobiJumpReader();
            this.movementReader = new ShinobiMovementReader();
        }

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
            var movement = movementReader.Read();
            if (movement == ShinobiMovementReader.Movement.Right)
            {
                rigidBody2D.velocity = new Vector2(Speed, rigidBody2D.velocity.y);
            }
            else if (movement == ShinobiMovementReader.Movement.Left)
            {
                rigidBody2D.velocity = new Vector2(-Speed, rigidBody2D.velocity.y);
            }
            else if (movement == ShinobiMovementReader.Movement.None)
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }
        }

        private void HandleJumping()
        {
            if (jumpReader.Read().Equals(ShinobiJumpReader.JumpState.Started))
            {
                isGrounded = false;
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, JumpSpeed);                
            }
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
                jumpReader.ResetJumpState();
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
            HandleMovement();
            HandleJumping();
        }
    }
}