using System;
using System.Threading.Tasks;
using Aquiris.Characters.Enemies.Enemies;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Enemy.IA;
using Aquiris.PacMan.Gameplay.State;
using Aquiris.PacMan.Level;
using Aquiris.PacMan.Player.Progress;
using Aquiris.PacMan.UI;
using Aquiris.PacMan.UI.StateMachine;
using Characters.Enemies;
using UnityEngine;

namespace Aquiris.PacMan.Gameplay
{
    internal static partial class Gameplay
    {
        #region Gameplay Events

        private static void OnStartScreen()
        {
            AudioManager.Instance.StopAll();
            
            ResetLives();
            HidePlayer();
            HideEnemies();
            
            PlayerProgressManager.LoadProgress();             
        }

        private static void OnStartConfirmation()
        {
            
        }

        private static async void OnLevelRestart()
        {
            AudioManager.Instance.OnLevelStart();
            
            ResetEnemiesStates();           
            FreezeCharacters();        
            HideEnemies();
            
            await Task.Delay(TimeSpan.FromSeconds(3.5F));
                        
            OnReady();  
        }

        private static async void OnLevelStart()
        {
            AudioManager.Instance.StopAll();
            AudioManager.Instance.OnLevelStart();          
            
            ResetEnemiesStates();           
            FreezeCharacters();        
            HideEnemies();
                        
            await Task.Delay(TimeSpan.FromSeconds(3.5F));
                        
            OnReady();                     
        }

        private async static void OnReady()
        {
            
            ShowCaracters();                       
            ResetEnemiesStates();        
            FreezeCharacters();

            await Task.Delay(TimeSpan.FromSeconds(GameConstants.GAMEPLAY_SCREEN_PRE_START_DURATION));
            
            ChangeGamePlayState(GamePlayState.GameStart);           
            ChangePlayerDirection(CharacterDirection.Left);
        }

        private static void OnGameStart()
        {            
            StartMovePlayer();
            StartMoveEnemies();
                        
            AudioManager.Instance.OnGameplay();                        
        }

        private async static void OnLevelCompleted()
        {
            AudioManager.Instance.StopAll();
            
            FreezeCharacters();           
            ResetEnemiesStates();
            
            await Task.Delay(TimeSpan.FromSeconds(GameConstants.GAMEPLAY_SCREEN_PRE_START_DURATION));
            
            HideEnemies();            
            RespawnPlayer();
            FreezeCharacters();
            HidePlayer();                                             
                        
            PlayerProgressManager.SaveProgress();
        }

        private static void OnLevelTransition()
        {
            //todo method to handler the level transition
        }

        public static void OnPlayerPowerUp()
        {
            AudioManager.Instance.OnPlayerPowerUp();
                       
            ChangeEnemiesSpeed(EnemySpeed.Low);
            ChangeEnemiesCondition(CharacterCondition.Blue);
            OnEnemiesBlue();        
        }

        public static void OnPowerUpTimeOut()
        {
            
        }    

        private static void OnGameOver()
        {
            ResetLives();            
            ResetEnemiesStates();
            ResetPaths();
            RespawnAllEnemies();
            RespawnPlayer();
            FreezeCharacters();
            HideEnemies();
            HidePlayer();
            ResetItems();
            ResetEnemiesSpeed();
            
            PlayerProgressManager.SaveProgress();
            
            UIStateManager.Instance.ChangeUIState(UIState.GameOver);
        }
        
        public static async void OnPlayerDie()
        {
            if (_lives == 0) {OnGameOver(); return;}
            
            ChangePlayerCondition(CharacterCondition.Dead);                           
            
            UIStateManager.Instance.ChangeUIState(UIState.LevelStart);
            
            AudioManager.Instance.StopAll();
            AudioManager.Instance.OnPlayerDie();
            
            DecreaseLifes();                                
            
            HUDManager.Instance.UpdateLiveUI(_lives);
            
            RespawnAllEnemies();
            ResetEnemiesStates();
            ResetPaths();
            AuthorizingPlayerMove();
            ChangePlayerDirection(CharacterDirection.Left);
            
            PlayerProgressManager.SaveProgress();
           
            await Task.Delay(TimeSpan.FromSeconds(GameConstants.PLAYER_RESPAWN_TIME));           
            
            RespawnPlayer();          
            ChangePlayerCondition(CharacterCondition.Freeze);
            
            AudioManager.Instance.OnGameplay();
        }

        public static void OnEnemyDie(GameObject enemy)
        {
            var targetEnemy = enemy.GetComponent<EnemyMovement>();
            
            AudioManager.Instance.OnEnemyDie();
                                   
            LevelManager.Instance.OnEmemyDie();
            
            targetEnemy.Character.State.ChangeConditionState(CharacterCondition.Dead);
            
            FreezeCharacters(GameConstants.FREEZE_TIME);
            
            targetEnemy.Respawn();           
        }

        #endregion
 
    }
}