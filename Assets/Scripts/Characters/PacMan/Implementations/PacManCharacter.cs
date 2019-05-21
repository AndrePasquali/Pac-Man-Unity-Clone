using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.Animation;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Input;

namespace DroidDigital.PacMan.Characters
{
    public sealed class PacManCharacter: Character
    {
        public InputController LinkedInputController => _linkedInputController ?? (_linkedInputController = GetComponent<InputController>());

        private InputController _linkedInputController;

        protected override void EveryFrame()
        {
            ProcessAnimator();
        }

        protected override void ProcessAnimator()
        {
            if(Animator == null || MovementCharacterController == null) return;
            
            AnimatorController.SetBool(Animator, GameConstants.MOVE_UP, State.DirectionState == CharacterDirection.Up);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_DOWN, State.DirectionState == CharacterDirection.Down);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_LEFT, State.DirectionState == CharacterDirection.Left);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_RIGHT, State.DirectionState == CharacterDirection.Right);
            AnimatorController.SetBool(Animator, GameConstants.IDLE, State.DirectionState == CharacterDirection.Null);

            AnimatorController.SetBool(Animator, GameConstants.ALIVE, State.ConditionState == CharacterCondition.Alive);
            AnimatorController.SetBool(Animator, GameConstants.DEAD, State.ConditionState == CharacterCondition.Dead);
            
            AnimatorController.SetFloat(Animator, GameConstants.HORIZONTAL_SPEED, MovementCharacterController.Speed.x);
            AnimatorController.SetFloat(Animator, GameConstants.VERTICAL_SPEED, MovementCharacterController.Speed.y);
            
            AnimatorController.SetAnimatorSpeed(Animator, State.DirectionState == CharacterDirection.Null ? 0 : 1);
        }
        
        private void ProcessInput()
        {
            if(Type != CharacterType.Player) return;

            var horizontalSpeed = LinkedInputController.Movement.x;
            var verticalSpeed = LinkedInputController.Movement.y;
            
            MovementCharacterController.SetHorizontalForce(horizontalSpeed);
            MovementCharacterController.SetVerticalForce(verticalSpeed);                
        }
    }
}