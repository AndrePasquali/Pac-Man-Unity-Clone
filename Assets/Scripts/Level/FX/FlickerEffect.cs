using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Aquiris.PacMan.FX
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlickerEffect: MonoBehaviour
    {
        [SerializeField] private float _flickerFrequency = 0.5F;
        
        private SpriteRenderer _sprite;
  
        private Text _animatedText;

        public enum Mode
        {
            Text,
            Sprite
        }

        public Mode CurrentMode = Mode.Sprite;

        public bool AutoStart = true;

        private bool isPicked;

        private void Start()
        {
            Initialize();        
        }

        private void Initialize()
        {
            if (CurrentMode == Mode.Sprite)
                _sprite = GetComponent<SpriteRenderer>();
            else _animatedText = GetComponent<Text>();
            
            if(AutoStart)
                StartFlicker();
        }

        protected void StartFlicker()
        {
            if (CurrentMode == Mode.Sprite)
                StartCoroutine(StartSpriteFlickerAsync());
            else StartCoroutine(StartTextFlickerAsync());
        }

        private IEnumerator StartSpriteFlickerAsync()
        {
            while (!isPicked)
            {
                var isActive = !_sprite.enabled;

                _sprite.enabled = isActive;
                
                yield return new WaitForSeconds(_flickerFrequency);
            }
        }
        
        private IEnumerator StartTextFlickerAsync()
        {
            while (!isPicked)
            {
                var isActive = !_animatedText.enabled;

                _animatedText.enabled = isActive;
                
                yield return new WaitForSeconds(_flickerFrequency);
            }
        }

        public void OnPicked()
        {
            isPicked = true;
        }

        public void OnResetLevel()
        {
            isPicked = false;
            StopAllCoroutines();
            StartFlicker();
        }
    }
}