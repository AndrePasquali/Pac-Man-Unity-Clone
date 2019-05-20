using DroidDigital.PacMan.UI;

namespace DroidDigital.Gameplay.Score
{
    internal static class ScoreManagement
    {
        private static int _score;

        private static int _highScore;

        public static void UpdateScore(int newScore)
        {
            _score += newScore;
            
            UpdateHighScore(_score);
            
            UIController.Instance.UpdateScoreUI(_score);
        }

        public static void UpdateHighScore(int newHighScore)
        {
            _highScore = newHighScore > _highScore ? newHighScore : _highScore;
            
            UIController.Instance.UpdateHighScoreUI(_highScore);
        }

        public static void Initialize()
        {
            UpdateScore(0);
        }
    }
}