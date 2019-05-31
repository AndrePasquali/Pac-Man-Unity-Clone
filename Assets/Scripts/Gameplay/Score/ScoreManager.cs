using Aquiris.PacMan.UI;

namespace Aquiris.Gameplay.Score
{
    internal static class ScoreManager
    {
        private static int _score;

        private static int _highScore;

        public static void UpdateScore(int newScore)
        {
            _score += newScore;
            
            UpdateHighScore(_score);
            
            HUDManager.Instance.UpdateScoreUI(_score);
        }

        public static void UpdateHighScore(int newHighScore)
        {
            _highScore = newHighScore > _highScore ? newHighScore : _highScore;
            
            HUDManager.Instance.UpdateHighScoreUI(_highScore);            
        }
       

        public static void Initialize()
        {
            UpdateScore(0);            
        }

        public static int GetHighestScore()
        {
            return _highScore;
        }        
    }
}