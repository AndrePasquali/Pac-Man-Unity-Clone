using System;
using System.Linq;
using System.Threading.Tasks;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.FX;

namespace DroidDigital.PacMan.Level.Item
{
    public class PowerUpItem: PickableItem
    {
        public FlickerEffect FlickerFX => _flickerFX ?? (_flickerFX = GetComponent<FlickerEffect>());

        private FlickerEffect _flickerFX;
        
        public async override void OnPick()
        {
            var enemies = FindObjectsOfType<EnemyMovement>().ToList();

            foreach (var enemie in enemies)
            {
                
                enemie.Character.State.ChangeConditionState(CharacterCondition.Vulnerable);

              //  enemie.OnPlayerPickPowerUp();

                enemie.Speed = enemie.Speed / 2;
            }
            
            FlickerFX.OnPicked();

            await Task.Delay(TimeSpan.FromSeconds(LevelManager.Instance.CurrentLevel.GhostBlueTime));
            
            DisableVunerability();
        }

        private void DisableVunerability()
        {
            var enemies = FindObjectsOfType<EnemyMovement>().ToList();

            foreach (var e in enemies)
            {
                e.Character.State.ChangeConditionState(CharacterCondition.Alive);
                e.Speed = e.Speed * 2;
            }
            
            AudioController.Instance.OnGameplay();
        }

        public override void PlayClip()
        {
            AudioController.Instance.PlayMusic(PickClip);
        }
    }
}