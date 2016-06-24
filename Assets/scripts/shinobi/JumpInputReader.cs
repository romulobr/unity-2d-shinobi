using System;
using UnityEngine;

namespace scripts.shinobi
{
    public class JumpInputReader
    {
        public enum JumpState
        {
            None,
            Started,
            Ended
        }

        private JumpState currentState = JumpState.None;
        private const float MaximumJumpDuration = 0.2f;
        private float jumpDuration = 0.0f;
        private bool readyToJump = true;

        public void ResetJumpState()
        {
            currentState = JumpState.None;
            jumpDuration = 0;            
        }

        public JumpState Read(float deltaTime)
        {
            var buttonIsPressedNow = (Input.GetButton("Jump") && Input.GetAxis("Jump") > 0);

            if (buttonIsPressedNow)
            {                
                if (currentState == JumpState.None && readyToJump)
                {
                    readyToJump = false;
                    currentState = JumpState.Started;
                    jumpDuration = 0;
                }
                if (currentState == JumpState.Started)
                {
                    currentState = JumpState.Started;
                    jumpDuration += deltaTime;
                    if (jumpDuration >= MaximumJumpDuration)
                    {
                        currentState = JumpState.Ended;
                    }
                }
            }
            else
            {
                if (currentState == JumpState.Started)
                {
                    currentState = JumpState.Ended;
                }
                else
                {
                    readyToJump = true;
                }
            }
            return currentState;
        }
    }
}