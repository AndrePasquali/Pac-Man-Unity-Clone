using DroidDigital.Gameplay.Score;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;

namespace DroidDigital.Core
{
    internal static class Initialize
    {
        public static void InitalizeGame()
        {
            GameplayManagement.Initialize();
            ScoreManagement.Initialize();
            CharacterStateManagement.Initialize();
        }
    }
}