using UnityEngine;

namespace DroidDigital.PacMan.Characters.State
{
    public class CharacterState: MonoBehaviour
    {
        public CharacterCondition ConditionState;

        public CharacterDirection DirectionState;

        public CharacterMovement MovementState;

        public void ChangeConditionState(CharacterCondition newConditionState)
        {
            ConditionState = newConditionState;
            
            if(newConditionState == CharacterCondition.Dead)
                ChangeDirectionState(CharacterDirection.Null);
        }

        public void ChangeDirectionState(CharacterDirection newDirectionState)
        {
            DirectionState = newDirectionState;
        }

        public void ChangeMovementState(CharacterMovement newMovementState)
        {
            MovementState = newMovementState;
        }
    }
}