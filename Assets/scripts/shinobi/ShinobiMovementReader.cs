using UnityEngine;

namespace scripts.shinobi
{
    public class ShinobiMovementReader
    {
        public enum Movement
        {
            Left,
            Right,
            None
        }

        public Movement Read()
        {
            var horizontal = Input.GetAxis("Horizontal");
            Movement detectedMovement;
            if (horizontal > 0.5f)
            {
                detectedMovement = Movement.Right;
            }
            else if (horizontal < -0.5f)
            {
                detectedMovement = Movement.Left;
            }
            else
            {
                detectedMovement = Movement.None;
            }
            return detectedMovement;
        }
    }
}
