namespace DroidDigital.PacMan.Level.Item
{
    public class DotItem: PickableItem
    {        
        public override void OnPick()
        {
            IsPicked = true;
            
            LevelManager.Instance.CheckDotsCount();
        }

        public override void PlayClip()
        {
            if(PickClip != null)
                AudioController.Instance.PlayEatingClip();
        }
    }
}