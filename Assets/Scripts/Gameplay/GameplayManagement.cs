using DroidDigital.Core.Constants;
using DroidDigital.PacMan.UI;

namespace DroidDigital.PacMan.Gameplay
{
    public static class GameplayManagement
    {
        private static int _lives;

        private delegate void OnGameOver();

        private static event OnGameOver OnGameOverAction;
        
        public static void Initialize()
        {
            OnGameOverAction += ResetLives;
            OnGameOverAction += UIController.Instance.OnGameOverUI;
            
            OnGameStart();
        }

        public static void OnPlayerDie()
        {
            if (_lives > 0)
            {
                _lives--;
                UIController.Instance.UpdateLiveUI(_lives);
            }
            else OnGameOverAction.Invoke();
        }

        private static void ResetLives()
        {
            _lives = GameConstants.MAX_LIVES;
        }

        private static void OnGameStart()
        {
            _lives = GameConstants.MAX_LIVES;
        }
    }
}