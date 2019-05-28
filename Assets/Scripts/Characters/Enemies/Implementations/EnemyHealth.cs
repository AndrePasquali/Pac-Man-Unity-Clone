using DroidDigital.Characters;
using DroidDigital.Core.Extensions;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.Gameplay;

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
            var movement = GetComponent<EnemyMovement>();
            
            movement.OnGameReset();
            movement.OnRespawn();   
        }
    }
}