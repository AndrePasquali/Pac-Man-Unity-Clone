using System.Linq;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.FX;

namespace DroidDigital.PacMan.Level.Item
{
    public class PowerUpItem: PickableItem
    {
        public FlickerEffect FlickerFX => _flickerFX ?? (_flickerFX = GetComponent<FlickerEffect>());

        private FlickerEffect _flickerFX;
        
        public override void OnPick()
        {
            var enemies = FindObjectsOfType<EnemyMovement>().ToList();

            foreach (var enemie in enemies)
            {
                enemie.Character.State.ChangeConditionState(CharacterCondition.Vulnerable);

                enemie.OnPlayerPickPowerUp();

                enemie.Speed = enemie.Speed / 2;
            }
            
            FlickerFX.OnPicked();
            
            Invoke("DisableVunerability", 8.0F);
        }

        private void DisableVunerability()
        {
            var enemies = FindObjectsOfType<EnemyMovement>().ToList();

            foreach (var e in enemies)
            {
                e.Character.State.ChangeConditionState(CharacterCondition.Alive);
                e.Speed = e.Speed * 2;
            }
        }

        public override void PlayClip()
        {
            AudioController.Instance.PlayMusic(PickClip);
        }
    }
}