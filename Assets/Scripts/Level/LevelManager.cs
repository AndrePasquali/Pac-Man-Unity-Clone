using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.Gameplay.State;
using DroidDigital.PacMan.Helpers;
using DroidDigital.PacMan.Level.Item;
using UnityEngine;

namespace DroidDigital.PacMan.Level
{
    public class LevelManager: Singleton<LevelManager>
    {
        public Level CurrentLevel;
        
        public List<Level> LevelsList = new List<Level>();

        public int LevelDotsCount => FindObjectsOfType<DotItem>().Count(e => !e.IsPicked);

        public SpriteRenderer LevelSprite;

        [SerializeField]
        private Sprite _levelFinishSprite;
        
        [SerializeField]
        private Sprite _levelSprite;

        public void OnLevelCompleted()
        {
            ProcessLevelFinishAnimation();
            
            GamePlayStateController.ChangeGamePlayState(GamePlayState.LevelCompleted);
        }

        public void SetCurrentLevel()
        {
            
        }

        public void SetNextLevel()
        {
            var currentLevelID = CurrentLevel.ID;
            
            if(CurrentLevel.AnimationAfterFinish)
                CallAnimation();

            CurrentLevel = LevelsList.FirstOrDefault(e => e.ID == currentLevelID + 1);
        }
        
        public int GetCurrentLevel()
        {
            return CurrentLevel.ID;
        }

        public void CallAnimation()
        {
            
        }

        public async void ProcessLevelFinishAnimation()
        {
            for (int i = 0; i < 4; i++)
            {
                LevelSprite.sprite = _levelFinishSprite;
                
                await Task.Delay(TimeSpan.FromSeconds(0.5F));

                LevelSprite.sprite = _levelSprite;
            }
        }

        public void CheckDotsCount()
        {
            if(LevelDotsCount == 0)
                OnLevelCompleted();
        }
    }
}