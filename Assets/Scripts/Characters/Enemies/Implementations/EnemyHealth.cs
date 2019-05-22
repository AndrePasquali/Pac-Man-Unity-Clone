using DroidDigital.Characters;

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
            gameObject.SetActive(false);
        }
    }
}