using Aquiris.Core.IO;
using Aquiris.Gameplay.Score;
using Aquiris.PacMan.Level;

namespace Aquiris.PacMan.Player.Progress
{
    public static class PlayerProgressManager
    {
        public static void SaveProgress()
        {
            var highestScore = ScoreManager.GetHighestScore();
            var longestLevel = LevelManager.Instance.GetCurrentLevel();
            
            var save = new Save
            {
                HighScore = highestScore,
                LongestLevel = longestLevel
            };
            
            BinarySave.Save(save);
        }

        public static void LoadProgress()
        {
            var progress = BinarySave.Load<Save>();
            
            UpdateHighScore(progress);
        }

        public static void UpdateHighScore(Save saveLoaded)
        {
            var highestScore = saveLoaded.HighScore;
            
            ScoreManager.UpdateHighScore(highestScore);
        }
    }
}