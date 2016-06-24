using UnityEngine;

namespace scripts.shinobi
{
    public class MovementInputReader
    {
        private const float Treshold = -0.01f;

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
            if (horizontal > 0.2f)
            {
                detectedMovement = Movement.Right;
            }
            else if (horizontal < Treshold)
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
