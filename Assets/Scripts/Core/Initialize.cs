using Aquiris.Gameplay.Score;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.PathFind;
using Aquiris.PacMan.Gameplay;

namespace Aquiris.Core
{
    internal static class Initialize
    {
        public static void InitalizeGame()
        {
            ScoreManager.Initialize();
            CharacterDirectionHelper.Initialize();
            WayPointManagement.PopulatePathList();
        }
    }
}