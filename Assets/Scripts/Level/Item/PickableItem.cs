using Aquiris.Core.Constants;
using Aquiris.Gameplay.Score;
using UnityEngine;

namespace Aquiris.PacMan.Level.Item
{
    public abstract class PickableItem : MonoBehaviour
    {
        public AudioClip PickClip;

        public SpriteRenderer SpriteRenderer => _spriteRenderer ?? (_spriteRenderer = GetComponent<SpriteRenderer>());

        private SpriteRenderer _spriteRenderer;

        public int BonusPointsAmount;

        public Collider2D Collider2D => _collider2D ?? (_collider2D = GetComponent<Collider2D>());

        private Collider2D _collider2D;

        public bool IsPicked
        {
            get { return _isPicked; }
            set
            {
                _isPicked = value;
                SpriteRenderer.enabled = !value;
                Collider2D.enabled = !value;
            }
        }

        private bool _isPicked;

        public delegate void OnPickEvent();

        public event OnPickEvent OnPickEventAction;

        protected virtual void Initialize()
        {
            OnPickEventAction += PlayClip;
            OnPickEventAction += OnPick;
            OnPickEventAction += AddScore;
        }

        private void Start()
        {
            Initialize();
        }

        public abstract void OnPick();

        public virtual void PlayClip()
        {
            if (PickClip == null) return;
            
            AudioManager.Instance.PlaySound(PickClip);     
        }

        public virtual void AddScore()
        {
            ScoreManager.UpdateScore(BonusPointsAmount);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag(GameConstants.PLAYER_TAG))
            {
                IsPicked = true;
                OnPickEventAction.Invoke();
            }

            OnTriggerCollider(collider);
        }

        protected virtual void OnTriggerCollider(Collider2D collider)
        {
            
        }
 
        
        private void OnDestroy()
        {
            OnPickEventAction -= PlayClip;
            OnPickEventAction -= OnPick;
            OnPickEventAction -= AddScore;
        }
    
    }
}