using Aquiris.Characters;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.State;
using UnityEngine;

namespace Aquiris.PacMan.Characters
{
    public class EnemyPhysics: CharacterPhysics
    {
        public override void OnCharacterCollider(Collider2D collider)
        {
            if(!collider.CompareTag(GameConstants.PLAYER_TAG)) return;
                        
            var currentState = GetComponent<CharacterState>();
                    
            if(currentState != null)
                if(currentState.ConditionState == CharacterCondition.Blue)
                    Kill(CharacterHealth);
           // else Kill(collider.gameObject.GetComponent<PacManHealth>());
        }
    }
}