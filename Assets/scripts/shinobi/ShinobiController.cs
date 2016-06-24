using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace scripts.shinobi
{
    public class ShinobiController : MonoBehaviour
    {
        private readonly MovementInputReader movementInputReader;
        private readonly JumpInputReader jumpInputReader;
        private readonly AttackInputReader attackInputReader;
        public float Speed = 0.5f;
        public float JumpSpeed = 1.0f;
        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        public float JumpDuration = 1000f;
        private bool isGrounded = false;

        public ShinobiController()
        {
            this.attackInputReader = new AttackInputReader();
            this.jumpInputReader = new JumpInputReader();
            this.movementInputReader = new MovementInputReader();
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

        private void HandleMovement(MovementInputReader.Movement movement)
        {
            if (movement == MovementInputReader.Movement.Right)
            {
                rigidBody2D.velocity = new Vector2(Speed, rigidBody2D.velocity.y);
            }
            else if (movement == MovementInputReader.Movement.Left)
            {
                rigidBody2D.velocity = new Vector2(-Speed, rigidBody2D.velocity.y);
            }
            else if (movement == MovementInputReader.Movement.None)
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }
        }

        private void HandleJumping(JumpInputReader.JumpState jumpState, float deltaTime)
        {
            if (jumpState.Equals(JumpInputReader.JumpState.Started))
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
                jumpInputReader.ResetJumpState();
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
            var deltaTime = Time.deltaTime;
            var movement = movementInputReader.Read();
            var jump = jumpInputReader.Read(deltaTime);
            var attack = attackInputReader.Read(deltaTime);

            Debug.Log(movement);
            Debug.Log(jump);
            Debug.Log(attack);

            HandleMovement(movement);
            HandleJumping(jump, deltaTime);
        }
    }
}