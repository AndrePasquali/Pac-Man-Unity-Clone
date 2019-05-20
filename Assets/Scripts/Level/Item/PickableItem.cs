using DroidDigital.Core.Constants;
using DroidDigital.Gameplay.Score;
using UnityEngine;

/*
 * PICKABLE ITEM ABSTRACT CLASS:
 * PROVIDE THE MAIN FUNCTIONS FOR ITEM BASED GAME OBJECTS
 *
 * PROGRAMMING BY ANDRE R. PASQUALI 
 * >>> DROID DIGITAL 2019 <<<<<
 */

namespace DroidDigital.PacMan.Level.Item
{
    public abstract class PickableItem : MonoBehaviour
    {
        public AudioClip PickClip;

        public SpriteRenderer SpriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());

        private SpriteRenderer _spriteRenderer;

        public int BonusPointsAmount;

        public delegate void OnPickEvent();

        public event OnPickEvent OnPickEventAction;

        private void Initialize()
        {
            OnPickEventAction += PlayClip;
            OnPickEventAction += OnPick;
            OnPickEventAction += AddScore;
            OnPickEventAction += DisableCollider;
            OnPickEventAction += DisableSprite;
        }

        private void Start()
        {
            Initialize();
        }

        public abstract void OnPick();

        public virtual void PlayClip()
        {
            if (PickClip == null) return;
            
            AudioController.Instance.PlaySound(PickClip);     
        }

        public virtual void AddScore()
        {
            ScoreManagement.UpdateScore(BonusPointsAmount);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag(GameConstants.PLAYER_TAG))
                OnPickEventAction.Invoke();
            
            OnTriggerCollider(collider);
        }

        protected virtual void OnTriggerCollider(Collider2D collider)
        {
            
        }
        
        private void DisableSprite()
        {
            SpriteRenderer.enabled = false;
        }

        private void DisableCollider()
        {
            var collider = GetComponent<CircleCollider2D>();

            collider.enabled = false;
        }
        
        private void OnDestroy()
        {
            OnPickEventAction -= PlayClip;
            OnPickEventAction -= OnPick;
            OnPickEventAction -= AddScore;
        }
    
    }
}