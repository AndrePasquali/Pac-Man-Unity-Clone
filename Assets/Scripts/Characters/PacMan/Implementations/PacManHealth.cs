using DroidDigital.Characters;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.Gameplay.State;
using DroidDigital.PacMan.UI.StateMachine;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public sealed class PacManHealth: CharacterHealth
    {
        public Collider2D Collider => _collider ?? (_collider = GetComponent<Collider2D>());

        private Collider2D _collider;

        public override void Kill()
        {
            base.Kill();
            
            OnDie();
                                  
            GamePlayStateController.ChangeGamePlayState(GamePlayState.PlayerDie);           
        }

        private void OnDie()
        {
            DisableCollider();
            
            UIStateController.Instance.ChangeUIState(UIState.StartGame);         
        }

        private void DisableCollider()
        {
            Collider.enabled = false;
        }
  
    }
}