using UnityEngine;

namespace scripts.shinobi
{
    public class ShinobiJumpReader
    {
        public enum JumpState
        {
            None,
            Started,
            Ended
        }

        private JumpState currentState = JumpState.None;

        public void ResetJumpState()
        {
            currentState = JumpState.None;
        }

        public JumpState Read()
        {
            var buttonIsPressed = Input.GetButton("Jump") && Input.GetAxis("Jump") > 0;

            if (buttonIsPressed)
            {
                if (currentState == JumpState.None)
                {
                    currentState = JumpState.Started;
                }
                if (currentState == JumpState.Started)
                {
                    currentState = JumpState.Started;
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