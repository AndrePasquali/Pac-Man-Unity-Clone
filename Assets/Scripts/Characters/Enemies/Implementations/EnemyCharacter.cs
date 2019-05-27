using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.Animation;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public sealed class EnemyCharacter: Character
    {        
        protected override void EveryFrame()
        {
            ProcessAnimator();
        }

        protected override void ProcessAnimator()
        {
            if(Animator == null || MovementCharacterController == null) return;
    
            AnimatorController.SetBool(Animator, GameConstants.MOVE_UP, State.ConditionState != CharacterCondition.Vulnerable && State.DirectionState == CharacterDirection.Up);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_DOWN, State.ConditionState != CharacterCondition.Vulnerable && State.DirectionState == CharacterDirection.Down);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_LEFT, State.ConditionState != CharacterCondition.Vulnerable && State.DirectionState == CharacterDirection.Left);
            AnimatorController.SetBool(Animator, GameConstants.MOVE_RIGHT, State.ConditionState != CharacterCondition.Vulnerable && State.DirectionState == CharacterDirection.Right);
            AnimatorController.SetBool(Animator, GameConstants.IDLE, State.DirectionState == CharacterDirection.Null);

            AnimatorController.SetBool(Animator, GameConstants.ALIVE, State.ConditionState == CharacterCondition.Alive);
            AnimatorController.SetBool(Animator, GameConstants.DEAD, State.ConditionState == CharacterCondition.Dead);
            
            AnimatorController.SetBool(Animator, GameConstants.VULNERABLE, State.ConditionState == CharacterCondition.Vulnerable);

            AnimatorController.SetFloat(Animator, GameConstants.HORIZONTAL_SPEED, MovementCharacterController.Speed.x);
            AnimatorController.SetFloat(Animator, GameConstants.VERTICAL_SPEED, MovementCharacterController.Speed.y);

            Animator.speed = State.ConditionState == CharacterCondition.Freeze ? 0 : MovementCharacterController.MovementSpeed;
        }
    }
}