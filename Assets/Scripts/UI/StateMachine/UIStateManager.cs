using System;
using System.Threading.Tasks;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.Animation;
using Aquiris.PacMan.Gameplay.State;
using Aquiris.PacMan.Helpers;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Gameplay;
using UnityEngine;

namespace Aquiris.PacMan.UI.StateMachine
{
    public class UIStateManager: Singleton<UIStateManager>
    {
        public UIState CurrentUIState = UIState.StartScreen;
       
        public Animator Animator;

        private void Start()
        {
            ChangeUIState(UIState.StartScreen);
        }

        private void Update()
        {
            ProcessAnimator();
            HandleStateBehaviours();
        }

        private void ProcessAnimator()
        {
            AnimatorController.SetBool(Animator, GameConstants.START_SCREEN, CurrentUIState == UIState.StartScreen);
            AnimatorController.SetBool(Animator, GameConstants.PREPARE_TO_START, CurrentUIState == UIState.StartScreenConfirmation);
            AnimatorController.SetBool(Animator, GameConstants.LEVEL_START, CurrentUIState == UIState.LevelStart);
            AnimatorController.SetBool(Animator, GameConstants.GAMESTART, CurrentUIState == UIState.GameStart);
            AnimatorController.SetBool(Animator, GameConstants.LEVEL_COMPLETED, CurrentUIState == UIState.LevelCompleted);
            AnimatorController.SetBool(Animator, GameConstants.GAMEOVER, CurrentUIState == UIState.GameOver);           
        }

        private void ProcessState()
        {
            switch (CurrentUIState)
            {
                    case UIState.StartScreen: OnStartScreen(); break;
                        case UIState.StartScreenConfirmation: OnPrepareToStart(); break;
                            case UIState.LevelStart: OnLevelStart(); break;
                                case UIState.GameStart: OnGameStart(); break;
                                    case UIState.LevelCompleted: OnLevelCompleted(); break;
                                        case UIState.GameOver: OnGameOver(); break;
            }
        }

        #region Events

        //Initial screen, when the game is show up
        private void OnStartScreen()
        {
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.StartScreen);
        }

        //Screen that confirms click or when the credits was inserted on original arcade
        private void OnPrepareToStart()
        {
            
        }

        //When the level is completed
        private void OnLevelCompleted()
        {
            
        }
        
        //When the maze is show up, preparation for start gameplay
        private void OnLevelStart()
        {            
            //AudioController.Instance.OnLevelStart();
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.LevelStart);
        }
        
        //Happens When the gameplay has started
        private void OnGameStart()
        {
            ChangeUIState(UIState.GameStart);
            
            AudioManager.Instance.OnGameplay();
            
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.GameStart);
        }

        //When the play no have more lifes and die
        private async void OnGameOver()
        {
            await Task.Delay(TimeSpan.FromSeconds(GameConstants.GAMEOVER_SCREEN_DURATION));
            
            OnReset();
        }

        private void OnReset()
        {
            ChangeUIState(UIState.StartScreen);
            
            GamePlayStateManager.ChangeGamePlayState(GamePlayState.StartScreen);
        }

        #endregion

        #region Handles

        public void ChangeUIState(UIState newUIState)
        {
            CurrentUIState = newUIState;
            
            ProcessState();
        }

        private void HandleStateBehaviours()
        {
            if (CurrentUIState == UIState.StartScreen && UnityEngine.Input.anyKeyDown)
                ChangeUIState(UIState.StartScreenConfirmation);
            else if (CurrentUIState == UIState.StartScreenConfirmation && UnityEngine.Input.anyKeyDown)
                ChangeUIState(UIState.LevelStart); 
        }

        #endregion         
    }
}