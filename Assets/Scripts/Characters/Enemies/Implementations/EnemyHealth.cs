using DroidDigital.Characters;
using DroidDigital.Core.Extensions;
using DroidDigital.PacMan.Characters.State;

namespace DroidDigital.PacMan.Characters
{
    public class EnemyHealth: CharacterHealth
    {
        public override void Kill()
        {            
            base.Kill();
            
            Invoke("DisableEnemy", 1F);
        }

        private void DisableEnemy()
        {            
            gameObject.Hide();
        }
    }
}