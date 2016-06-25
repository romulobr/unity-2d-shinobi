using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace scripts.shinobi
{
    public class ShinobiController : MonoBehaviour
    {
        private readonly MovementInputReader movementInputReader;
        private readonly JumpInputReader jumpInputReader;
        private readonly AttackInputReader attackInputReader;
        private readonly AnimationDirector animationDirector;

        private Animator animator;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidBody2D;
        public Text DebugText;

        public float Speed = 0.5f;
        public float JumpSpeed = 1.0f;

        public ShinobiController()
        {
            this.animationDirector = new AnimationDirector();
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

        private void HandleMovement(MovementInputReader.Movement movement)
        {
            if (movement == MovementInputReader.Movement.Right)
            {
                spriteRenderer.flipX = false;
                rigidBody2D.velocity = new Vector2(Speed, rigidBody2D.velocity.y);
            }
            else if (movement == MovementInputReader.Movement.Left)
            {
                rigidBody2D.velocity = new Vector2(-Speed, rigidBody2D.velocity.y);
                spriteRenderer.flipX = true;
            }
            else if (movement == MovementInputReader.Movement.None)
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
            }
        }

        private void HandleJumping(JumpInputReader.JumpState jumpState)
        {
            if (jumpState == JumpInputReader.JumpState.Started)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, JumpSpeed);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (rigidBody2D.velocity.y <= 0.1)
            {
                jumpInputReader.ResetJumpState();
            }
        }

        public void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;
            var movement = movementInputReader.Read();
            var jump = jumpInputReader.Read(deltaTime);
            var attack = attackInputReader.Read(deltaTime);
            var selectedAnimation = animationDirector.SelectAnimationFor(movement, jump, attack);

            DebugText.text = selectedAnimation+" > "+movement.ToString()+" / Jump"+jump.ToString() + " / " + attack.ToString();
            HandleMovement(movement);
            HandleJumping(jump);
            
            animator.Play(selectedAnimation);
        }
    }
}