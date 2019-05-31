using System;
using System.Threading.Tasks;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Level;
using UnityEngine;

namespace Aquiris.PacMan.Characters.State
{
    public class CharacterState: MonoBehaviour
    {
        public CharacterCondition ConditionState;

        public CharacterDirection DirectionState;

        private bool _enemyIsBlue;

        public async void OnEnemyBlue()
        {
            var timeCounter = 0F;
            var blueTime = LevelManager.Instance.CurrentLevel.GhostBlueTime;

            _enemyIsBlue = true;

            while (timeCounter <= blueTime)
            {
                timeCounter += Time.deltaTime;
                
                if(ConditionState == CharacterCondition.Dead)
                    return;
                if (ConditionState == CharacterCondition.Freeze)
                    await Task.Delay(TimeSpan.FromSeconds(GameConstants.FREEZE_TIME));
                
                ConditionState = CharacterCondition.Blue;

                await Task.Delay(TimeSpan.FromSeconds(0.01F));
            }

            _enemyIsBlue = false;
            
            ChangeConditionState(CharacterCondition.Normal);

            var currentSpeed = Gameplay.Gameplay.GetCurrentSpeed();
            
            AudioManager.Instance.PlayByEnemySpeed(currentSpeed);
        }

        public void ChangeConditionState(CharacterCondition newConditionState)
        {         
            if(_enemyIsBlue && (newConditionState != CharacterCondition.Freeze || 
                                newConditionState != CharacterCondition.Dead)) return;
            
            ConditionState = newConditionState;
            
            if(newConditionState == CharacterCondition.Dead)
                ChangeDirectionState(CharacterDirection.Null);
        }

        public void ChangeDirectionState(CharacterDirection newDirectionState)
        {
            DirectionState = newDirectionState;
        }
    }
}