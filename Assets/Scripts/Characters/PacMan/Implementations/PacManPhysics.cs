using DroidDigital.Characters;
using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay.State;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public class PacManPhysics: CharacterPhysics
    {    
        public override void OnCharacterCollider(Collider2D collider)
        {
            var isEnemieColliding = collider.CompareTag(GameConstants.ENEMY_TAG);
            
            if(!isEnemieColliding || GamePlayStateController.CurrentGamePlayState != GamePlayState.InGame) return;
                    
            if(!collider.CompareTag(GameConstants.ENEMY_TAG)) return;
            
            Debug.Log(collider.name);


            var enemieState = collider.GetComponent<CharacterState>();
            
            if(enemieState == null) return;
                                    
            if(enemieState.ConditionState == CharacterCondition.Vulnerable
               || enemieState.ConditionState == CharacterCondition.Dead) 
                return;
                        
            Kill(CharacterHealth);                              
        }       
    }
}