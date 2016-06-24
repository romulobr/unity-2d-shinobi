namespace scripts.shinobi
{
    public class AnimationDirector
    {
        public string SelectAnimationFor(MovementInputReader.Movement movement, JumpInputReader.JumpState jump,
            AttackInputReader.AttackState attack)
        {
            var animation = "idle";
            if (attack == AttackInputReader.AttackState.Attacking)
            {
                animation = "attack";
            }
            else if (jump != JumpInputReader.JumpState.None)
            {
                animation = "jump";
            }
            else if (movement != MovementInputReader.Movement.None)
            {
                animation = "run";
            }
            return animation;
        }
    }
}
