using DroidDigital.Gameplay.Score;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.PathFind;

namespace DroidDigital.Core
{
    internal static class Initialize
    {
        public static void InitalizeGame()
        {
            GameplayManagement.Initialize();
            ScoreManagement.Initialize();
            CharacterStateManagement.Initialize();
            WayPointManagement.PopulatePathList();
        }
    }
}