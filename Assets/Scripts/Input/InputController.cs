using DroidDigital.Core.Constants;
using UnityEngine;

namespace DroidDigital.PacMan.Input
{
    public class InputController: MonoBehaviour
    {
        public Vector2 Movement => _movement;
        
        private Vector2 _movement;

        public bool SmoothMovement = true;

        public bool IsEnable = true;

        private void Update()
        {
            ProcessMovement();
        }

        private void ProcessMovement()
        {
            if(!IsEnable) return;
            
            SetMovement();
        }

        protected void SetHorizontalMovement(float horizontalInput)
        {
            _movement.x = horizontalInput;
        }

        protected void SetVerticalMovement(float verticalMovement)
        {
            _movement.y = verticalMovement;
        }

        protected void SetMovement()
        {
            var horizontalAxis = SmoothMovement
                ? UnityEngine.Input.GetAxis(GameConstants.HORIZONTAL_AXIS)
                : UnityEngine.Input.GetAxisRaw(GameConstants.HORIZONTAL_AXIS);

            var verticalAxis = SmoothMovement
                ? UnityEngine.Input.GetAxis(GameConstants.VERTICAL_AXIS)
                : UnityEngine.Input.GetAxisRaw(GameConstants.VERTICAL_AXIS);
            
            SetHorizontalMovement(horizontalAxis);
            SetVerticalMovement(verticalAxis);                   
        }      
    }
}