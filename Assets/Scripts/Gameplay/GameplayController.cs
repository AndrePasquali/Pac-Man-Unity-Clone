using System;
using System.Linq;
using System.Threading.Tasks;
using DroidDigital.Core.Constants;
using DroidDigital.Core.Extensions;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.Gameplay.State;
using DroidDigital.PacMan.Level.Item;
using DroidDigital.PacMan.PathFind;
using DroidDigital.PacMan.UI;
using DroidDigital.PacMan.UI.StateMachine;
using UnityEngine;
using CharacterMovement = DroidDigital.PacMan.Characters.CharacterMovement;

namespace DroidDigital.PacMan.Gameplay
{
    public static class GameplayController
    {
        private static int _lives;

        private static GameObject _player;

        public static GameObject Player = _player ?? (_player = GameObject.FindWithTag(GameConstants.PLAYER_TAG));

        private static GameObject _enemies;

        public static GameObject Enemies =
            _enemies ?? (_enemies = GameObject.Find("PlayGround").transform.Find("Enemies").gameObject);
        

        public static void ProcessState()
        {
            switch (GamePlayStateController.CurrentGamePlayState)
            {
                    case GamePlayState.Start: OnStart(); break;
                        case GamePlayState.StartConfirmation: OnStartConfirmation(); break;
                            case GamePlayState.PreGame: OnPreGame(); break;
                                case GamePlayState.InGame: OnInGame(); break;
                                    case GamePlayState.LevelCompleted: OnLevelCompleted(); break;
                                        case GamePlayState.LevelTransition: OnLevelTransition(); break;
                                            case GamePlayState.PlayerDie: OnPlayerDie(); break;
                                                case GamePlayState.PlayerGetPowerUp: OnPlayerGetPowerUp(); break;
                                                    case GamePlayState.GameOver: OnGameOver(); break;
                            
            }
        }

        #region Gameplay Events

        private static void OnStart()
        {
            ResetLives();
            HidePlayer();
            HideEnemies();
        }

        private static void OnStartConfirmation()
        {
            
        }

        private static async void OnPreGame()
        {
            AudioController.Instance.OnLevelStart();
            
            await Task.Delay(TimeSpan.FromSeconds(3.5F));
                        
            OnReady();                     
        }

        private async static void OnReady()
        {
            ShowCaracters();
                        
            ResetEnemiesStates();
            
            FreezeCharacters();

            await Task.Delay(TimeSpan.FromSeconds(GameConstants.GAMEPLAY_SCREEN_PRE_START_DURATION));
            
            ChangeGamePlayState(GamePlayState.InGame);
            
            ChangePlayerDirection(CharacterDirection.Left);
        }

        private static void OnInGame()
        {            
            StartMovePlayer();
            StartMoveEnemies();
            
            AudioController.Instance.OnGameplay();
        }

        private static void OnLevelCompleted()
        {
            AudioController.Instance.StopAll();
            
            ResetEnemiesStates();
            HideEnemies();
            HidePlayer();
        }

        private static void OnLevelTransition()
        {
            
        }

        private static void OnPlayerGetPowerUp()
        {
            
        }

        private static void OnGameOver()
        {
            ResetLives();            
            ResetEnemiesStates();       
            HideEnemies();
            HidePlayer();
            
            HUDController.Instance.OnGameOverUI();            
        }
        
        public static async void OnPlayerDie()
        {
            if (_lives == 0) {OnGameOver(); return;}
            
            AudioController.Instance.StopAll();
            AudioController.Instance.OnPlayerDie();
            
            DecreaseLifes();                                
            HUDController.Instance.UpdateLiveUI(_lives);
            ChangePlayerCondition(CharacterCondition.Dead);                           
            RespawnAllEnemies();
            ResetEnemiesStates();
            ResetDots();
           
            await Task.Delay(TimeSpan.FromSeconds(GameConstants.PLAYER_RESPAWN_TIME));           
            
            RespawnPlayer();          
            ChangePlayerCondition(CharacterCondition.Freeze);
            AudioController.Instance.OnGameplay();
        }


        #endregion
  

        public static void ChangePlayerCondition(CharacterCondition newCondition)
        {
            var state = Player.GetComponent<CharacterState>();
            
            state.ChangeConditionState(newCondition);
        }

        private static void ChangePlayerDirection(CharacterDirection newDirection)
        {
            var state = Player.GetComponent<CharacterState>();
            
            state.ChangeDirectionState(CharacterDirection.Left);
        }

        public static void ChangeEnemieCondition(CharacterCondition newCondition)
        {
            var enemies = Enemies.GetComponentsInChildren<EnemyMovement>().ToList();

            foreach (var enemy in enemies) enemy.Character.State.ChangeConditionState(newCondition);       
        }

        private static void ResetLives()
        {
            _lives = GameConstants.MAX_LIVES;
        }

        public static void HideEnemies()
        {
            Enemies.Hide();
        }

        public static void ShowEnemies()
        {
            Enemies.Show();
        }

        public static void ShowPlayer()
        {
            Player.Show();
        }

        public static void HidePlayer()
        {
            Player.Hide();
        }

        public static void ShowCaracters()
        {
            ShowPlayer();
            ShowEnemies();
        }

        public static void ResetEnemiesStates()
        {
            var enemies = Enemies.GetComponentsInChildren<EnemyMovement>().ToList();

            if(enemies != null)
                foreach (var enemy in enemies)
                {
                    enemy.gameObject.SetActive(true);
                    
                    enemy.OnGameReset();
                }       
        }
        
        private static void RespawnAllEnemies()
        {            
            var enemies = GameObject.FindObjectsOfType<EnemyMovement>();

            foreach (var enemy in enemies)
                enemy.OnRespawn(); 
        }

        private static void RespawnPlayer()
        {
            var pacman = Player.GetComponent<CharacterMovement>();
            
            pacman.Respawn();
        }

        private static void StartMoveEnemies()
        {
            ChangeEnemieCondition(CharacterCondition.Alive);
        }

        private static void StartMovePlayer()
        {
            ChangePlayerCondition(CharacterCondition.Alive);
        }

        private static void ResetDots()
        {
            var dots = GameObject.FindObjectsOfType<ItemPath>().Where(e => e.Type == ItemPath.PathType.Home).ToList();

            foreach (var dot in dots) dot.OnEnemieRespawn();  
        }

        private static void FreezeCharacters()
        {
            ChangeEnemieCondition(CharacterCondition.Freeze);
            ChangePlayerCondition(CharacterCondition.Freeze);
        }

        private static void DecreaseLifes()
        {
            _lives--;
        }

        private static void ChangeGamePlayState(GamePlayState newState)
        {
            GamePlayStateController.ChangeGamePlayState(newState);
        }
    }
}