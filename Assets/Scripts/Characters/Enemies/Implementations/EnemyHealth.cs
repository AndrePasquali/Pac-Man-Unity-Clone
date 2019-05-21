using DroidDigital.Characters;

namespace DroidDigital.PacMan.Characters
{
    public class EnemyHealth: CharacterHealth
    {
        public override void Kill()
        {            
            //base.Kill();           
            gameObject.SetActive(false);
        }    
    }
}