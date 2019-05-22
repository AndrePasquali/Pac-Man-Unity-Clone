using DroidDigital.Characters;
using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.State;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public class PacManPhysics: CharacterPhysics
    {    
        public override void OnCharacterCollider(Collider2D collider)
        {
            var isEnemieColliding = collider.CompareTag(GameConstants.ENEMY_TAG);
            
            if(!isEnemieColliding) return;

            var enemieState = collider.GetComponent<CharacterState>();
                        
            if(enemieState.ConditionState == CharacterCondition.Vulnerable
               || enemieState.ConditionState == CharacterCondition.Dead) 
                return;
                        
            Kill(CharacterHealth);                              
        }       
    }
}