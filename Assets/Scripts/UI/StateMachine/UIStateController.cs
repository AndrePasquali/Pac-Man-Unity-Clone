using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.Animation;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.Gameplay.State;
using DroidDigital.PacMan.Helpers;
using UnityEngine;

namespace DroidDigital.PacMan.UI.StateMachine
{
    public class UIStateController: Singleton<UIStateController>
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
            AnimatorController.SetBool(Animator, GameConstants.PREPARE_TO_START, CurrentUIState == UIState.PrepareToStart);
            AnimatorController.SetBool(Animator, GameConstants.START_GAME, CurrentUIState == UIState.StartGame);
            AnimatorController.SetBool(Animator, GameConstants.GAMEPLAY, CurrentUIState == UIState.Gameplay);
            AnimatorController.SetBool(Animator, GameConstants.LEVEL_COMPLETED, CurrentUIState == UIState.LevelCompleted);
            AnimatorController.SetBool(Animator, GameConstants.GAMEOVER, CurrentUIState == UIState.GameOver);           
        }

        private void ProcessState()
        {
            switch (CurrentUIState)
            {
                    case UIState.StartScreen: OnStartScreen(); break;
                        case UIState.PrepareToStart: OnPrepareToStart(); break;
                            case UIState.StartGame: OnPreGame(); break;
                                case UIState.Gameplay: OnGameplay(); break;
                                    case UIState.LevelCompleted: OnLevelCompleted(); break;
                                        case UIState.GameOver: OnGameOver(); break;
            }
        }

        #region Events

        //Initial screen, when the game is show up
        private void OnStartScreen()
        {
            GamePlayStateController.ChangeGamePlayState(GamePlayState.Start);
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
        private void OnPreGame()
        {            
            //AudioController.Instance.OnLevelStart();
            
            GamePlayStateController.ChangeGamePlayState(GamePlayState.PreGame);    
        }
        
        //When the gameplay has started
        private void OnGameplay()
        {
            ChangeUIState(UIState.Gameplay);
            
            AudioController.Instance.OnGameplay();
            
            GamePlayStateController.ChangeGamePlayState(GamePlayState.InGame);
        }

        //When the play no have more lifes and die
        private void OnGameOver()
        {
            GamePlayStateController.ChangeGamePlayState(GamePlayState.GameOver);
            
            Invoke("OnPreGame", GameConstants.GAMEOVER_SCREEN_DURATION);
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
                ChangeUIState(UIState.PrepareToStart);
            else if (CurrentUIState == UIState.PrepareToStart && UnityEngine.Input.anyKeyDown)
                ChangeUIState(UIState.StartGame); 
        }

        #endregion         
    }
}