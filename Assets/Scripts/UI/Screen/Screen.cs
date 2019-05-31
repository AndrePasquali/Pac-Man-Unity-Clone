using UnityEngine;

namespace Aquiris.PacMan.UI
{
    public class Screen: MonoBehaviour
    {
        public enum ScreenName
        {
            GameStart,
            GameOver
        }

        public ScreenName Name;

        public CanvasGroup Canvas;

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                Canvas.alpha = value ? 1 : 0;
                _isEnable = value;
            }
        }

        private bool _isEnable;
    }
}