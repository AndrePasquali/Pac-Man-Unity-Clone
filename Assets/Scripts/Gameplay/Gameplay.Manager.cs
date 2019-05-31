using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Enemy.IA;
using Aquiris.PacMan.Input;
using Aquiris.PacMan.PathFind;
using Characters.Enemies;
using Aquiris.Core.Extensions;
using Aquiris.PacMan.FX;
using UnityEngine;
using CharacterMovement = Aquiris.PacMan.Characters.CharacterMovement;

namespace Aquiris.PacMan.Gameplay
{
    internal static partial class Gameplay
    {    
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

        public static void ResetPowerUps()
        {
            foreach (var powerUp in PowerUpList)
            {
                powerUp.IsPicked = false;
                powerUp.GetComponent<FlickerEffect>().OnResetLevel();
            }                     
        }
        
        public static void ResetEnemiesStates()
        {         
            if(EnemyList != null)
                foreach (var enemy in EnemyList)
                {
                    enemy.gameObject.Show();
                    
                    enemy.GetComponent<EnemyMovement>().OnGameReset();
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
            
            AuthorizingPlayerMove();
        }

        private static void StartMoveEnemies()
        {
            ChangeEnemiesCondition(CharacterCondition.Normal);
        }

        private static void StartMovePlayer()
        {
            ChangePlayerCondition(CharacterCondition.Normal);
        }
        
        public static void FreezeCharacters()
        {
            ChangeEnemiesCondition(CharacterCondition.Freeze);
            ChangePlayerCondition(CharacterCondition.Freeze);
        }

        public async static void FreezeCharacters(float timeToUnFreeze)
        {
            ChangeEnemiesCondition(CharacterCondition.Freeze);
            ChangePlayerCondition(CharacterCondition.Freeze);

            await Task.Delay(TimeSpan.FromSeconds(timeToUnFreeze));
            
            ChangeEnemiesCondition(CharacterCondition.Normal);
            ChangePlayerCondition(CharacterCondition.Normal);          
        }

        public static void OnEnemiesBlue()
        {
            if(EnemyList != null) foreach (var enemy in EnemyList) enemy.GetComponent<CharacterState>().OnEnemyBlue();
        }

        public static void ResetPaths()
        {
          //  var paths = GameObject.FindObjectsOfType<ItemPath>().Where(e => e.Type == ItemPath.PathType.Home).ToList();

            var homePaths = ItemPathsList.Where(e => e.Type == ItemPath.PathType.Home).ToList();

            foreach (var path in homePaths) path.OnEnemieRespawn();  
        }

        public static void ResetEnemiesSpeed()
        {
            foreach (var enemy in EnemyList) 
                enemy.GetComponent<EnemyMovement>().CurrentEnemySpeed = EnemySpeed.Normal;
            
            SetCurrentEnemiesSpeed(EnemySpeed.Normal);
        }

        public static void PlayEnemyScoreAnimation()
        {
            
        }

        public static void ChangeEnemiesSpeed(EnemySpeed newSpeed)
        {
            if(newSpeed <= _currentEnemySpeed) return;
            
            foreach (var enemy in EnemyList) 
                enemy.GetComponent<EnemyMovement>().CurrentEnemySpeed = newSpeed;
            
            SetCurrentEnemiesSpeed(newSpeed);

            switch (newSpeed)
            {
                    case EnemySpeed.Normal: AudioManager.Instance.OnNormalSpeed(); break;
                        case EnemySpeed.Moderate: AudioManager.Instance.OnModerateSpeed(); break;
                            case EnemySpeed.Fast: AudioManager.Instance.OnFastSpeed(); break;
                                case EnemySpeed.SuperFast: AudioManager.Instance.OnSuperFastSpeed(); break;
            }
        }

        private static void SetCurrentEnemiesSpeed(EnemySpeed currentSpeed)
        {
            _currentEnemySpeed = currentSpeed;
        }

        public static EnemySpeed GetCurrentSpeed()
        {
            return _currentEnemySpeed;
        }
            

        public static void ResetItems()
        {
            foreach (var dot in DotItemsList) dot.IsPicked = false;
            
            ResetPowerUps();
        }

        public static int GetCurrentCombo()
        {
            var currentCombo = EnemyList.Count(e => e.State.ConditionState == CharacterCondition.Blue);

            return currentCombo;
        }

        private static void DecreaseLifes()
        {
            _lives--;
        }
        
        private static void AuthorizingPlayerMove()
        {
            var move = Player.GetComponent<InputController>();

            move.AuthorizingMove = true;
        }
    }
}