using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.Animation;
using Aquiris.PacMan.Gameplay.State;
using Aquiris.PacMan.Helpers;
using Aquiris.PacMan.Level.Item;
using Aquiris.PacMan.UI.StateMachine;
using Characters.Enemies;
using UnityEngine;

namespace Aquiris.PacMan.Level
{
    public class LevelManager: Singleton<LevelManager>
    {
        public Level CurrentLevel;
        
        public List<Level> LevelsList = new List<Level>();

        private int _enemiesKilledCount;

        public int LevelDotsCount
        {
            get {return FindObjectsOfType<DotItem>().Count(e => e.IsPicked);}
            set { _levelDotsCount = value; }
        }

        private int _levelDotsCount;

        [SerializeField]
        private Animator _levelAnimator;

        [SerializeField]
        private Sprite _levelFinishSprite;

        public void OnGameOver()
        {
            CurrentLevel = LevelsList.First(e => e.ID == 1);
        }

        public void OnEmemyDie()
        {
            _enemiesKilledCount++;
            
            HandleEnemySpeed();
        }

        private void HandleEnemySpeed()
        {          
            switch (_enemiesKilledCount)
            {
                    case 3: Gameplay.Gameplay.ChangeEnemiesSpeed(EnemySpeed.Moderate); break;
                        case 5: Gameplay.Gameplay.ChangeEnemiesSpeed(EnemySpeed.Fast); break;
                            case 10: Gameplay.Gameplay.ChangeEnemiesSpeed(EnemySpeed.SuperFast); break;
                                case 12: Gameplay.Gameplay.ChangeEnemiesSpeed(EnemySpeed.UltraFast); break;
            }
        }

        private void ResetEnemyKilledCount()
        {
            _enemiesKilledCount = 0;
        }
        
  
        public void OnLevelCompleted()
        {                        
            ProcessLevelFinishAnimation();
            
            ResetEnemyKilledCount();
            
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.LevelCompleted);
        }

        public void SetCurrentLevel(int id)
        {
            CurrentLevel = LevelsList.FirstOrDefault(e => e.ID == id);
        }

        public async void SetNextLevel()
        {             
            if(CurrentLevel.AnimationAfterFinish)
                CallAnimation();

            CurrentLevel = LevelsList.FirstOrDefault(e => e.ID == GetCurrentLevel() + 1);

            await Task.Delay(TimeSpan.FromSeconds(GameConstants.LEVEL_COMPLETED_ANIMATION_DURATION));
            
            UIStateManager.Instance.ChangeUIState(UIState.LevelStart);
                        
            AnimatorController.SetBool(_levelAnimator, GameConstants.ENDGAME_ANIMATION, false);
      
            AnimatorController.SetBool(_levelAnimator, GameConstants.GAMESTART_ANIMATION, true);

            ResetLevelDotsCount();
        }

        private void ResetLevelDotsCount()
        {
            LevelDotsCount = GameConstants.MAX_DOTS;
            
            Gameplay.Gameplay.ResetItems();
        }
        
        public int GetCurrentLevel()
        {
            return CurrentLevel.ID;
        }

        private void StartNewLevel()
        {
            SetCurrentLevel(GetCurrentLevel() + 1);
            
            UIStateManager.Instance.ChangeUIState(UIState.LevelStart);
        }

        public void CallAnimation()
        {
            
        }

        public void ProcessLevelFinishAnimation()
        {
           AnimatorController.SetBool(_levelAnimator, GameConstants.GAMESTART_ANIMATION, false);    
           AnimatorController.SetBool(_levelAnimator, GameConstants.ENDGAME_ANIMATION, true);
            
           SetNextLevel();         
        }

        public void CheckDotsCount()
        {
            if(LevelDotsCount == GameConstants.MAX_DOTS)
                OnLevelCompleted();                          
        }
    }
}