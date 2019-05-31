using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Gameplay.State;

namespace Aquiris.PacMan.Gameplay
{
    internal static partial class Gameplay
    {
        public static void ProcessState()
        {
            switch (GamePlayStateManager.CurrentGamePlayState)
            {
                case GamePlayState.StartScreen: OnStartScreen(); break;
                case GamePlayState.StartConfirmation: OnStartConfirmation(); break;
                case GamePlayState.LevelStart: OnLevelStart(); break;
                case GamePlayState.GameStart: OnGameStart(); break;
                case GamePlayState.LevelCompleted: OnLevelCompleted(); break;
                case GamePlayState.LevelTransition: OnLevelTransition(); break;
                case GamePlayState.PlayerDie: OnPlayerDie(); break;
                case GamePlayState.PlayerPowerUp: OnPlayerPowerUp(); break;
                case GamePlayState.PowerUpTimeOut: OnPowerUpTimeOut(); break;
                case GamePlayState.GameOver: OnGameOver(); break;                           
            }
        }      

        public static void ChangePlayerCondition(CharacterCondition newCondition)
        {
            var state = Player.GetComponent<CharacterState>();
            
            state.ChangeConditionState(newCondition);
        }

        private static void ChangePlayerDirection(CharacterDirection newDirection)
        {
            var state = Player.GetComponent<CharacterState>();
            
            state.ChangeDirectionState(newDirection);
        }

        public static void ChangeEnemiesCondition(CharacterCondition newCondition)
        {         
            foreach (var enemy in EnemyList) enemy.State.ChangeConditionState(newCondition);       
        }
        
        private static void ChangeGamePlayState(GamePlayState newState)
        {
            GamePlayStateManager.ChangeGamePlayState(newState);
        }
    }
}