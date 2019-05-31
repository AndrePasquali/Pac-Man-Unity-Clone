using Aquiris.PacMan.Gameplay.State;

namespace Aquiris.PacMan.Level.Item
{
    public class DotItem: PickableItem
    {        
        public override void OnPick()
        {
            LevelManager.Instance.CheckDotsCount();
        }

        public override void PlayClip()
        {
            if(PickClip != null)
                AudioManager.Instance.PlayEatingClip();
        }
    }
}