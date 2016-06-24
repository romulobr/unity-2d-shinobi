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
       
        public JumpState Read()
        {
            var currentState = JumpState.None;
            if (Input.GetButton("Jump") && Input.GetAxis("Jump") > 0)
            {
                currentState = JumpState.Started;
            };
            return currentState;
        }
    }
}