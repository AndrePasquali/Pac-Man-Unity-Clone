using DroidDigital.Characters;
using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.State;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public class EnemyPhysics: CharacterPhysics
    {
        public override void OnCharacterCollider(Collider2D collider)
        {
            if(!collider.CompareTag(GameConstants.PLAYER_TAG)) return;
                     
            var currentState = GetComponent<CharacterState>();
                    
            if(currentState != null)
                if(currentState.ConditionState == CharacterCondition.Vulnerable)
                    Kill(CharacterHealth);
        }
    }
}