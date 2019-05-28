using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DroidDigital.PacMan.Characters;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;
using DroidDigital.PacMan.PathFind;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        public List<CharacterDirection> InitialAllowedDirections = new List<CharacterDirection>();
        
        public List<Vector2> VectorDirectionList = new List<Vector2>{Vector2.left, Vector2.right, Vector2.down, Vector2.up};

        private ItemPath _lastDetectedPath;

        private Vector3 _startPosition;

        [SerializeField]
        private float _timeToStartMove = 2.5F;
        
        private void Start()
        {
            Initialize();
        }

        private bool CanWalk()
        {
            return (Character.State.ConditionState != CharacterCondition.Freeze && Character.State.ConditionState != CharacterCondition.Dead);
        }

        private void AuthorizingWalk()
        {
            ResetDirections();
            ResetState();      
        }

        private void ResetState()
        {
            Character.State.ChangeConditionState(CharacterCondition.Alive);
        }

        private void FixedUpdate()
        {            
            AILogic();            
        }   

        private void Initialize()
        {
            StoreInitialPosition();
            StoreInitialDirections();
            
            if(!CanWalk()) Invoke("AuthorizingWalk", _timeToStartMove);
        }

        private void StoreInitialPosition()
        {
            _startPosition = transform.position;
        }

        private void StoreInitialDirections()
        {
            InitialAllowedDirections = AllowedDirections;
        }

        public async void OnRespawn()
        {
            Character.State.ChangeConditionState(CharacterCondition.Freeze);
            
            ResetPosition();
            
            SetVisibility(false);

            await Task.Delay(TimeSpan.FromSeconds(2.0F));
            
            Respawn();
        }

        public async void Respawn()
        {
            SetVisibility(true);

            await Task.Delay(TimeSpan.FromSeconds(_timeToStartMove));
                              
            AuthorizingWalk();
        }

        public void OnGameReset()
        {
            GameplayController.ResetPaths();
        
            Character.State.ChangeConditionState(CharacterCondition.Alive);
        
            Character.State.ChangeConditionState(CharacterCondition.Freeze);
            
            ResetDirections();
            
            Character.State.ChangeDirectionState(AllowedDirections[Random.Range(0, AllowedDirections.Count)]);
            
            ResetPosition();
        }

        private void ResetPosition()
        {
            transform.position = _startPosition;
        }

        private void ResetDirections()
        {
            AllowedDirections = InitialAllowedDirections;
        }

        public void SetVisibility(bool isVisible)
        {
            var sprite = GetComponent<SpriteRenderer>();

            sprite.enabled = isVisible;
        }

        public void AILogic()
        {
            if(!CanWalk()) return;
            
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
            
            //var targetDirection = AllowedDirections.First(e => e != currentDirection.DirectionState);

            //currentDirection.ChangeDirectionState(targetDirection);
            
            switch (currentDirection.DirectionState)
            {
                    case CharacterDirection.Down: if(AllowedDirections.Contains(CharacterDirection.Up)) currentDirection.ChangeDirectionState(CharacterDirection.Up); break;
                        case CharacterDirection.Up: if(AllowedDirections.Contains(CharacterDirection.Down)) currentDirection.ChangeDirectionState(CharacterDirection.Down); break;
                            case CharacterDirection.Left: if(AllowedDirections.Contains(CharacterDirection.Right)) currentDirection.ChangeDirectionState(CharacterDirection.Right); break;
                                case CharacterDirection.Right: if(AllowedDirections.Contains(CharacterDirection.Left)) currentDirection.ChangeDirectionState(CharacterDirection.Left); break;
                                    default: currentDirection.ChangeDirectionState(currentDirection.DirectionState); break;
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