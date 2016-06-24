using System;
using UnityEngine;

namespace scripts.shinobi
{
    public class AttackInputReader
    {
        public enum AttackState
        {
            Attacking,
            None
        }

        private AttackState currentState = AttackState.None;
        private const float MaximumAttackDuration = 0.2f;
        private const string AttackButton = "Fire1";
        private float attackDuration = 0.0f;
        private bool readyToAttack = true;

        public AttackState Read(float deltaTime)
        {
            var buttonIsPressedNow = (Input.GetButton(AttackButton) && Input.GetAxis(AttackButton) > 0);

            if (readyToAttack && buttonIsPressedNow && currentState == AttackState.None)
            {
                readyToAttack = false;
                currentState = AttackState.Attacking;
                attackDuration = 0;

            }
            if (currentState == AttackState.Attacking)
            {
                attackDuration += deltaTime;
                if (attackDuration >= MaximumAttackDuration)
                {
                    currentState = AttackState.None;
                }
            }
            else if(!buttonIsPressedNow)
            {
                if (currentState == AttackState.Attacking)
                {
                    currentState = AttackState.None;
                }
                else
                {
                    readyToAttack = true;
                }
            }
            return currentState;
        }
    }
}