using DroidDigital.Characters;
using DroidDigital.PacMan.Characters.State;

namespace DroidDigital.PacMan.Characters
{
    public class EnemyHealth: CharacterHealth
    {
        public override void Kill()
        {            
            base.Kill();

            CharacterState.ChangeConditionState(CharacterCondition.Freeze);
            
            Invoke("DisableEnemy", 1F);
        }

        private void DisableEnemy()
        {
            CharacterState.ChangeConditionState(CharacterCondition.Dead);
            
            gameObject.SetActive(false);
        }
    }
}