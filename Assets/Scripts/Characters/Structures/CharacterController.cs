using UnityEngine;

/*
 * CHARACTER CONTROLLER CLASS:
 * PROVIDE THE PHYSICS DATA AND HANDLE PHYSICS BASED MOVEMENTS FROM CHARACTERS
 *
 * PROGRAMMING BY ANDRE R. PASQUALI 
 * >>> DROID DIGITAL 2019 <<<<<
 */

namespace DroidDigital.PacMan
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        //Is Left Colliding?
        private bool IsCollidingLeft;

        //IsColliding Right?
        private bool IsCollidingRight;

        //IsColliding Front?
        private bool IsCollidingFront;

        //IsColliding Back
        private bool IsCollidingBack;
        
        public Rigidbody2D Rigibody => _rigibody ?? (_rigibody = GetComponent<Rigidbody2D>());

        private Rigidbody2D _rigibody;

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        private Vector2 _speed;

        [Range(0.1F, 10.0F)]
        public float MovementSpeed = 0.5F;

        [HideInInspector]
        public Vector2 NewPosition;

        [HideInInspector]
        public Vector2 CurrentPosition;

        public void EveryFrame()
        {
            ProcessPosition();
        }

        private void FixedUpdate()
        {
            //EveryFrame();
        }

        public void MoveToPosition(Vector2 newPosition)
        {
            Rigibody.MovePosition(newPosition);
        }

        public void SetForce(Vector2 newSpeed)
        {
            _speed = newSpeed;
        }

        public void SetHorizontalForce(float newHorizontalForce)
        {
            _speed.x = newHorizontalForce;
        }

        public void SetVerticalForce(float newVerticalForce)
        {
            _speed.y = newVerticalForce;
        }

        public void AddHorizontalForce(float newForceToAdd)
        {
            _speed.x += newForceToAdd;
        }

        public void AddVerticalForce(float newForceToAdd)
        {
            _speed.y += newForceToAdd;
        }

        public void ProcessPosition()
        {
            CurrentPosition = NewPosition;
            
            NewPosition = Speed * Time.deltaTime * MovementSpeed;
        }
    }
}
