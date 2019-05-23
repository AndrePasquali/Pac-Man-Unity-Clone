using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters.Animation;
using DroidDigital.PacMan.Helpers;
using UnityEngine;

namespace DroidDigital.PacMan.UI.StateMachine
{
    public class UIStateController: Singleton<UIStateController>
    {
        public UIState CurrentUIState = UIState.Idle;

        public Animator Animator;

        [SerializeField]
        private GameObject _characters;

        private void Update()
        {
            ProcessAnimator();
            HandleStateBehaviours();
        }

        private void ProcessAnimator()
        {
            AnimatorController.SetBool(Animator, GameConstants.IDLE_SCREEN, CurrentUIState == UIState.Idle);
            AnimatorController.SetBool(Animator, GameConstants.PREPARE_TO_START, CurrentUIState == UIState.PrepareToStart);
            AnimatorController.SetBool(Animator, GameConstants.START_GAME, CurrentUIState == UIState.StartGame);
            AnimatorController.SetBool(Animator, GameConstants.GAMEPLAY, CurrentUIState == UIState.Gameplay);
            AnimatorController.SetBool(Animator, GameConstants.LEVEL_COMPLETED, CurrentUIState == UIState.LevelCompleted);
            AnimatorController.SetBool(Animator, GameConstants.GAMEOVER, CurrentUIState == UIState.GameOver);           
        }

        public void ChangeUIState(UIState newUIState)
        {
            CurrentUIState = newUIState;
        }

        private void OnLevelStart()
        {                        
            ChangeUIState(UIState.Gameplay);
        }

        private void HandleStateBehaviours()
        {
            if (CurrentUIState == UIState.Idle && UnityEngine.Input.GetMouseButtonDown(0))
                ChangeUIState(UIState.PrepareToStart);
            else if (CurrentUIState == UIState.PrepareToStart && UnityEngine.Input.GetMouseButtonDown(0))
            {
                ChangeUIState(UIState.StartGame);
                
                AudioController.Instance.OnLevelStart();
                
                Invoke("TurnOnCharacters", 2.0F);
            }
        }

        private void TurnOnCharacters()
        {
            _characters.SetActive(true);
            
            Invoke("OnLevelStart", 3.0F);
        }
    }
}