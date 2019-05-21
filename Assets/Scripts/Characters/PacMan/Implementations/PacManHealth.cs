using DroidDigital.Characters;
using DroidDigital.PacMan.Gameplay;

namespace DroidDigital.PacMan.Characters
{
    public sealed class PacManHealth: CharacterHealth
    {
        public override void Kill()
        {
            base.Kill();
            
            GameplayManagement.OnPlayerDie();
        }
    }
}