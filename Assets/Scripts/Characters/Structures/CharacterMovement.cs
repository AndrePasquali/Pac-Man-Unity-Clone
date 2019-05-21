using System;
using System.Collections.Generic;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.PathFind;
using PathFind;
using UnityEngine;

namespace DroidDigital.PacMan.Characters
{
    public class CharacterMovement: MonoBehaviour
    {
        public float MovementPointSpeed = 1.0F;
        
        public CharacterController CharacterController => _characterController ?? (_characterController = GetComponent<CharacterController>());

        protected CharacterController _characterController;

        public PacManCharacter Character => _character ?? (_character = GetComponent<PacManCharacter>());

        private PacManCharacter _character;      
                
        public Animator Animator => _animator ?? (_animator = GetComponent<Animator>());

        private Animator _animator;

        protected int _currentWayPoint = 0;

        protected float _lastUpdateTime;

        public LayerMask ObstaclesLayer;

        public List<CharacterDirection> AllowedDirections = new List<CharacterDirection>();
        
        public List<Vector2> VectorDirectionList = new List<Vector2>{Vector2.left, Vector2.right, Vector2.down, Vector2.up};
   

        public void ProcessMovement()
        {            
            var controller = CharacterController;         

            var desiredDirection = GetTargetDirection();
            
            if(!IsValidDirection()) return;

            var desiredPosition = Vector2.MoveTowards(transform.position, (Vector2)transform.position + desiredDirection,
                controller.MovementSpeed);
            
            controller.Rigibody.MovePosition(desiredPosition);
            
            //transform.Translate(Vector2.MoveTowards(controller.CurrentPosition, controller.NewPosition, controller.MovementPointSpeed), Space.Self);            
        }

        private void ProcessMove()
        {
            if(Character.State.ConditionState == CharacterCondition.Dead) return;
            
            var direction = (Vector3) CharacterStateManagement.GetVectorByDirectionState(Character.State.DirectionState);
            
            if(!AllowedDirections.Contains(Character.State.DirectionState)) return;

            transform.position = transform.position + direction * Time.fixedDeltaTime * MovementPointSpeed;
        }

        public void SetDirectionByInput()
        {
            var currentInputSpeed = Character.LinkedInputController.Movement;

            if (currentInputSpeed.x > 0)
            {
                if(IsValidDirection(CharacterDirection.Right))
                ChangeDirection(CharacterDirection.Right);
            }
            else if (currentInputSpeed.x < 0)
            {
                if(IsValidDirection(CharacterDirection.Left))
                ChangeDirection(CharacterDirection.Left);
            }
            else if (currentInputSpeed.y > 0)
            {
                if(IsValidDirection(CharacterDirection.Up))
                ChangeDirection(CharacterDirection.Up);
            }
            else if (currentInputSpeed.y < 0)
            {
                if(IsValidDirection(CharacterDirection.Down))
                ChangeDirection(CharacterDirection.Down);
            }
            else if(currentInputSpeed.x == 0 || currentInputSpeed.y == 0) ChangeDirection(Character.State.DirectionState);                    
        }

        public void ChangeDirection(CharacterDirection newDirection)
        {
            Character.State.ChangeDirectionState(newDirection);
        }
              
        
        public void UpdateAllowedDirections(List<CharacterDirection> characterDirections)
        {
            AllowedDirections = characterDirections;
        }

        protected bool IsValidDirection()
        {
            return AllowedDirections.Contains(Character.State.DirectionState);
        }

        protected bool IsValidDirection(CharacterDirection direction)
        {
            return AllowedDirections.Contains(direction);
        }
        
        protected bool IsValidDirection(Vector2 direction)
        {
            var characterDirection = CharacterStateManagement.GetDirectionByVector(direction);
            
            return AllowedDirections.Contains(characterDirection);
        }

        private Vector2 GetTargetDirection()
        {
            switch (Character.State.DirectionState)
            {
                    case CharacterDirection.Down: return Vector2.down;
                        case CharacterDirection.Up: return Vector2.up;
                            case CharacterDirection.Left: return Vector2.left;
                                case CharacterDirection.Right: return Vector2.right;
                                    default: return Vector2.zero;
            }
        }

        private void Start()
        {
           PopulateDirections();
        }

        public void Initialize()
        {
            WayPointManagement.PopulatePathList();
            PopulateDirections();
        }

        private void PopulateDirections()
        {
            foreach (CharacterDirection direction in Enum.GetValues(typeof(CharacterDirection)))
            {
                if(direction == CharacterDirection.Null) continue;
                AllowedDirections.Add(direction);
            }
        }

        public void EveryFrame()
        {
            SetDirectionByInput();
            ProcessMove();            
        }

        private void FixedUpdate()
        {
           EveryFrame();
        }
        
    }
}