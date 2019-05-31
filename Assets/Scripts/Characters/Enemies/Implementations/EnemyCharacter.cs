using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.Animation;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Gameplay;
using UnityEngine;

namespace Aquiris.PacMan.Characters
{
    public sealed class EnemyCharacter: Character
    {        
        protected override void EveryFrame()
        {
            ProcessAnimator();
        }

        //Handle the character animations by character machine state
        protected override void ProcessAnimator()
        {
            if(Animator == null || MovementCharacterController == null) return;
    
            AnimatorController.SetBool(Animator, GameConstants.MOVE_UP, State.ConditionState != CharacterCondition.Blue && State.DirectionState == CharacterDirection.Up);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_DOWN, State.ConditionState != CharacterCondition.Blue && State.DirectionState == CharacterDirection.Down);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_LEFT, State.ConditionState != CharacterCondition.Blue && State.DirectionState == CharacterDirection.Left);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_RIGHT, State.ConditionState != CharacterCondition.Blue && State.DirectionState == CharacterDirection.Right);
            AnimatorController.SetBool(Animator, GameConstants.IDLE, State.DirectionState == CharacterDirection.Null);

            AnimatorController.SetBool(Animator, GameConstants.ALIVE, State.ConditionState == CharacterCondition.Normal);
            AnimatorController.SetBool(Animator, GameConstants.DEAD, State.ConditionState == CharacterCondition.Dead);
            
            AnimatorController.SetBool(Animator, GameConstants.BLUE, State.ConditionState == CharacterCondition.Blue);

            AnimatorController.SetFloat(Animator, GameConstants.HORIZONTAL_SPEED, MovementCharacterController.Speed.x);
            AnimatorController.SetFloat(Animator, GameConstants.VERTICAL_SPEED, MovementCharacterController.Speed.y);

            Animator.speed = State.ConditionState == CharacterCondition.Freeze ? 0 : MovementCharacterController.MovementSpeed;
        }
    }
}