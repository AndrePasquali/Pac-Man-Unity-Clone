using DroidDigital.Core.IO;
using DroidDigital.Gameplay.Score;
using DroidDigital.PacMan.Level;

namespace DroidDigital.PacMan.Player.Progress
{
    public static class PlayerProgressManager
    {
        public static void SaveProgress()
        {
            var highestScore = ScoreManagement.GetHighestScore();
            var longestLevel = LevelManager.Instance.GetCurrentLevel();
            
            var save = new Save
            {
                HighScore = highestScore,
                LongestLevel = longestLevel
            };
            
            SaveManager.Save(save);
        }

        public static void LoadProgress()
        {
            var progress = SaveManager.Load<Save>();
            
            UpdateHighScore(progress);
        }

        public static void UpdateHighScore(Save saveLoaded)
        {
            var highestScore = saveLoaded.HighScore;
            
            ScoreManagement.UpdateHighScore(highestScore);
        }
    }
}