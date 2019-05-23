using DroidDigital.Characters;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.UI.StateMachine;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public sealed class PacManHealth: CharacterHealth
    {
        public CircleCollider2D Collider => _collider ?? (_collider = GetComponent<CircleCollider2D>());

        private CircleCollider2D _collider;

        public override void Kill()
        {
            base.Kill();
            
            OnDie();
                                  
            GameplayManagement.OnPlayerDie();            
        }

        private void OnDie()
        {
            Collider.enabled = false;
            
            UIStateController.Instance.ChangeUIState(UIState.StartGame);
            
            RespawnAllEnemies();
            
            Invoke("RespawnPlayer", 3.0F);
        }

        private void RespawnAllEnemies()
        {
            var enemies = FindObjectsOfType<EnemyMovement>();

            foreach (var enemy in enemies)
                enemy.OnRespawn(); 
        }

        private void RespawnPlayer()
        {
            var pacman = GetComponent<CharacterMovement>();
            
            pacman.Respawn();
        }
    }
}