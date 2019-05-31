using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.Animation;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Input;

namespace Aquiris.PacMan.Characters
{
    public sealed class PacManCharacter: Character
    {
        public InputController LinkedInputController => _linkedInputController ?? (_linkedInputController = GetComponent<InputController>());

        private InputController _linkedInputController;
        
        private void OnEnable()
        {
            AnimatorController.SetBool(Animator, GameConstants.ISPACMAN, Name == CharacterName.PacMan);
        }

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
          //  AnimatorController.SetBool(Animator, GameConstants.IDLE, State.MovementState == Characters.State.CharacterMovement.Idle);
            
            AnimatorController.SetBool(Animator, GameConstants.ALIVE, State.ConditionState == CharacterCondition.Normal);
            AnimatorController.SetBool(Animator, GameConstants.DEAD, State.ConditionState == CharacterCondition.Dead);
            AnimatorController.SetBool(Animator, GameConstants.IDLE, State.ConditionState == CharacterCondition.Freeze);
            
            AnimatorController.SetFloat(Animator, GameConstants.HORIZONTAL_SPEED, MovementCharacterController.Speed.x);
            AnimatorController.SetFloat(Animator, GameConstants.VERTICAL_SPEED, MovementCharacterController.Speed.y);
            
            //AnimatorController.SetAnimatorSpeed(Animator, State.DirectionState == CharacterDirection.Null ? 0 : 1);
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