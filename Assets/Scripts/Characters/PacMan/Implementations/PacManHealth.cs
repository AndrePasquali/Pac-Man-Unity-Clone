using Aquiris.Characters;
using Aquiris.PacMan.Gameplay.State;
using UnityEngine;

namespace Aquiris.PacMan.Characters
{
    public class PacManHealth: CharacterHealth
    {
        public override void Kill()
        {
            base.Kill();
                                                          
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.PlayerDie);           
        }
    }
}