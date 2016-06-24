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

        public void ResetJumpState()
        {
            currentState = JumpState.None;
            jumpDuration = 0;
        }

        public JumpState Read(float deltaTime)
        {
            var buttonIsPressed = Input.GetButton("Jump") && Input.GetAxis("Jump") > 0;

            if (buttonIsPressed)
            {
                if (currentState == JumpState.None)
                {
                    currentState = JumpState.Started;
                    jumpDuration = 0;
                }
                if (currentState == JumpState.Started)
                {
                    currentState = JumpState.Started;
                    jumpDuration += deltaTime;
                    Debug.Log(jumpDuration);
                    if (jumpDuration >= MaximumJumpDuration)
                    {
                        currentState = JumpState.Ended;
                    }
                }
                if (currentState == JumpState.Ended)
                {
                    currentState = JumpState.Ended;
                }
                return currentState;
            }
            else
            {
                if (currentState == JumpState.Started)
                {
                    currentState = JumpState.Ended;
                }
            }

            return currentState;
        }
    }
}