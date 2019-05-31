using Aquiris.Characters;
using Aquiris.Core.Extensions;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Enemy.IA;
using Aquiris.PacMan.Gameplay;

namespace Aquiris.PacMan.Characters
{
    public class EnemyHealth: CharacterHealth
    {
        public override void Kill()
        {            
            base.Kill();
            Gameplay.Gameplay.OnEnemyDie(gameObject);
        }
    }
}