using Aquiris.PacMan.FX;
using Aquiris.PacMan.Gameplay.State;

namespace Aquiris.PacMan.Level.Item
{
    public class PowerUpItem: PickableItem
    {
        public FlickerEffect FlickerFX => _flickerFX ?? (_flickerFX = GetComponent<FlickerEffect>());

        private FlickerEffect _flickerFX;
        
        public override void OnPick()
        {
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.PlayerPowerUp);
               
            FlickerFX.OnPicked();            
        }
   
        public override void PlayClip()
        {
            //AudioManager.Instance.PlayMusic(PickClip);
        }
    }
}