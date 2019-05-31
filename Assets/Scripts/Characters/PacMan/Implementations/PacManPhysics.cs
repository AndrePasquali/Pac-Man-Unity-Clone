using Aquiris.Characters;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Gameplay.State;
using UnityEngine;

namespace Aquiris.PacMan.Characters
{
    public sealed class PacManPhysics: CharacterPhysics
    {    
        public override void OnCharacterCollider(Collider2D collider)
        {
            var isEnemieColliding = collider.CompareTag(GameConstants.ENEMY_TAG);
                        
            if(!isEnemieColliding) return; 
             
            var enemieState = collider.GetComponent<CharacterState>();
                      
            if(enemieState == null) return;
                                               
            if(enemieState.ConditionState == CharacterCondition.Blue
               || enemieState.ConditionState == CharacterCondition.Dead) 
                return;          
                        
            Kill(CharacterHealth);                              
        }       
    }
}