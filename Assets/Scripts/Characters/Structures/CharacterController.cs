using Aquiris.PacMan.Characters.State;
using UnityEngine;

namespace Aquiris.PacMan
{
    [RequireComponent(typeof(Rigidbody2D))]
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

        public Collider2D Collider2D => _collider2D ?? (_collider2D = GetComponent<Collider2D>());
        
        private Collider2D _collider2D;

        private float _colliderTop => Collider2D.offset.y + (Collider2D.bounds.size.y / 2F);

        private float _colliderBottom => Collider2D.offset.y - (Collider2D.bounds.size.y / 2F);

        private float _colliderLeft => Collider2D.offset.x - (Collider2D.bounds.size.x / 2F);

        private float _colliderRight => Collider2D.offset.x + (Collider2D.bounds.size.x / 2F);
        
        private Vector2 _colliderTopLeft => new Vector2(_colliderLeft, _colliderTop);
        
        private Vector2 _colliderTopRight => new Vector2(_colliderRight, _colliderTop);
        
        private Vector2 _colliderBottomLeft => new Vector2(_colliderLeft, _colliderBottom);
        
        private Vector2 _colliderBottomRight => new Vector2(_colliderRight, _colliderBottom);
        
        private Vector2 _colliderCenter => new Vector2(Collider2D.bounds.center.x, Collider2D.bounds.center.y);

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

        public void ProcessContacts()
        {
            
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
