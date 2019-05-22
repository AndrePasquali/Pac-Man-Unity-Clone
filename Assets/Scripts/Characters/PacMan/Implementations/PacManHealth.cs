using DroidDigital.Characters;
using DroidDigital.PacMan.Gameplay;
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
                      
            GameplayManagement.OnPlayerDie();
            
            OnDie();
        }

        private void OnDie()
        {
            Collider.enabled = false;
        }
    }
}