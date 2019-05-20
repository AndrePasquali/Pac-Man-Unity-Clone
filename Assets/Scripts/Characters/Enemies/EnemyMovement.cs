using System.Collections;
using System.Collections.Generic;
using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.PathFind;
using PathFind;
using UnityEngine;
using CharacterMovement = DroidDigital.PacMan.Characters.CharacterMovement;

namespace DroidDigital.PacMan.Enemy.IA
{
    public class EnemyMovement: MonoBehaviour
    {
        
        [Range(0, 10)]
        public float Speed = 5.0F;

        public ItemPath NextRoute;

        public EnemyCharacter Character => _character ?? (_character = GetComponent<EnemyCharacter>());

        private EnemyCharacter _character;

        public CharacterController Controller => _controller ?? (_controller = GetComponent<CharacterController>());

        private CharacterController _controller;

        private int _currentWayPoint;

        private float _lastUpdateTime;
        
        public List<CharacterDirection> AllowedDirections = new List<CharacterDirection>();
        
        public List<Vector2> VectorDirectionList = new List<Vector2>{Vector2.left, Vector2.right, Vector2.down, Vector2.up};

        private ItemPath _lastDetectedPath;
        
        private void Start()
        {
            Initialize();
        }

        private void FixedUpdate()
        {
            AILogic();            
        }   

        private void Initialize()
        {
            WayPointManagement.PopulatePathList();
        }

        public void AILogic()
        {
            var direction = (Vector3) CharacterStateManagement.GetVectorByDirectionState(Character.State.DirectionState);

            transform.position = transform.position + direction * Time.deltaTime * Speed;
        }

        public IEnumerator WayPoint()
        {
            while (true)
            {
                if(WayPointManagement.WayPointQueue.Count == 0)
                    WayPointManagement.PopulateQueue();
                
                var direction = WayPointManagement.WayPointQueue.Dequeue();
                
                var desiredPosition = Vector2.MoveTowards(transform.position,
                    direction.transform.position, Speed);
                
                Character.State.DirectionState = CharacterStateManagement.GetDirectionByVector(desiredPosition);
                                                
                Controller.MoveToPosition(desiredPosition);
                
                yield return new WaitForEndOfFrame();
            }
        }

        public void ProcessAIMovementByNode()
        {
         //   if ((Time.time - _lastUpdateTime) <= 0.01F) return;

           // _lastUpdateTime = Time.time;
            
            //var controller = CharacterController;

            Vector2 choosedDirection = VectorDirectionList[Random.Range(0, VectorDirectionList.Count)];                            
                
         //   var desiredPosition = Vector2.MoveTowards(transform.position,
           //     transform.position + (Vector3)choosedDirection, controller.MovementPointSpeed);
            
            if(!IsValidDirection(choosedDirection)) return;

            Character.State.DirectionState = CharacterStateManagement.GetDirectionByVector(choosedDirection);
                                                
            Controller.MoveToPosition(transform.position + (Vector3)choosedDirection);     
        }
        
        public void OnPlayerPickPowerUp()
        {
            var currentDirection = Character.State;

            switch (currentDirection.DirectionState)
            {
                    case CharacterDirection.Down: currentDirection.ChangeDirectionState(CharacterDirection.Up); break;
                        case CharacterDirection.Up: currentDirection.ChangeDirectionState(CharacterDirection.Down); break;
                            case CharacterDirection.Left: currentDirection.ChangeDirectionState(CharacterDirection.Right); break;
                                case CharacterDirection.Right: currentDirection.ChangeDirectionState(CharacterDirection.Left); break;
                                    default: currentDirection.ChangeDirectionState(CharacterDirection.Left); break;
            }  
        }


        public void ProcessAIMovement()
        {       
            var wayPoints = WayPointManagement.WayPointList;
                                   
            if (_currentWayPoint >= wayPoints.Count)
                _currentWayPoint = 0;
            
            if (transform.position != wayPoints[_currentWayPoint].PathTransform.position)
            {
                var currentWayPointPosition = WayPointManagement.WayPointList[_currentWayPoint].transform.position;
                
                var desiredPosition = Vector2.MoveTowards(transform.position,
                    currentWayPointPosition, Speed);
                
                Character.State.DirectionState = CharacterStateManagement.GetDirectionByVector(desiredPosition);
                                                
                Controller.MoveToPosition(desiredPosition);
            }
            else
            {
                if ((Time.time - _lastUpdateTime) >= 0.0F)
                {
                    _lastUpdateTime = Time.time;
                    _currentWayPoint++;
                }
            }                    
        }
        
        protected bool IsValidDirection(Vector2 direction)
        {
            var characterDirection = CharacterStateManagement.GetDirectionByVector(direction);
            
            return AllowedDirections.Contains(characterDirection);
        }
            
        protected bool IsValidDirection()
        {
            return AllowedDirections.Contains(Character.State.DirectionState);
        }
        
        public void UpdateAllowedDirections(List<CharacterDirection> characterDirections)
        {
            AllowedDirections = characterDirections;
        }
    }
}