using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlickerEffect: MonoBehaviour
    {
        [SerializeField] private float _flickerFrequency = 0.5F;
        
        protected SpriteRenderer Sprite => _sprite ?? (_sprite = GetComponent<SpriteRenderer>());

        private SpriteRenderer _sprite;

        public bool AutoStart = true;

        private bool isPicked;

        private void Start()
        {
            if(AutoStart)
                StartFlicker();
        }

        protected void StartFlicker()
        {
            StartCoroutine(StartFlickerAsync());
        }

        private IEnumerator StartFlickerAsync()
        {
            while (!isPicked)
            {
                var isActive = !Sprite.enabled;

                Sprite.enabled = isActive;
                
                yield return new WaitForSeconds(_flickerFrequency);
            }
        }

        public void OnPicked()
        {
            isPicked = true;
        }
    }
}