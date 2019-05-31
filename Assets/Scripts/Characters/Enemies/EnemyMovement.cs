using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.PathFind;
using Characters.Enemies;
using Aquiris.PacMan.Gameplay;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aquiris.PacMan.Enemy.IA
{
    public class EnemyMovement: MonoBehaviour
    {        
        [Range(0, 10)]
        //Movement Speed
        public float Speed = 5.0F;

        //I will use this later for waypoint system
        public ItemPath NextRoute;

        public EnemyCharacter Character => _character ?? (_character = GetComponent<EnemyCharacter>());

        private EnemyCharacter _character;

        public CharacterController Controller => _controller ?? (_controller = GetComponent<CharacterController>());

        private CharacterController _controller;

        private int _currentWayPoint;

        private float _lastUpdateTime;

        public EnemySpeed CurrentEnemySpeed
        {
            get { return _currentEnemySpeed; }
            set
            {
                _currentEnemySpeed = value;
                Speed = CurrentSpeed;
            }
        }

        private EnemySpeed _currentEnemySpeed;

        public float CurrentSpeed
        {
            get
            {
                switch (CurrentEnemySpeed)
                {
                        case EnemySpeed.Low: return GameConstants.LOW_SPEED;
                        case EnemySpeed.Normal: return GameConstants.NORMAL_SPEED;
                        case EnemySpeed.Moderate: return GameConstants.MODERATE_SPEED;
                        case EnemySpeed.Fast: return GameConstants.FAST_SPEED;
                        case EnemySpeed.SuperFast: return GameConstants.SUPER_FAST_SPEED;
                        case EnemySpeed.UltraFast: return GameConstants.ULTRA_FAST_SPEED;
                        default: return GameConstants.NORMAL_SPEED;
                }
            }
        }

        private float _currentSpeed;
        
        //The current directions allowed. Is updated by item paths
        public List<CharacterDirection> AllowedDirections = new List<CharacterDirection>();
        
        //We make cache of initial allowed directions to restore later
        public List<CharacterDirection> InitialAllowedDirections = new List<CharacterDirection>();
        
        //The list of possible vectors directions
        public List<Vector2> VectorDirectionList = new List<Vector2>{Vector2.left, Vector2.right, Vector2.down, Vector2.up};

        //last detected path. I will use this later for waypoint system
        private ItemPath _lastDetectedPath;

        //The start position. The unity bug sometimes the intial positions  
        private Vector3 _startPosition
        {
            get
            {
                switch (Character.Name)
                {
                        case CharacterName.Blinky: return GameConstants.INITIAL_BLINKY_POSITION;
                            case CharacterName.Clyde: return GameConstants.INITIAL_CLYDE_POSITION;
                                case CharacterName.Inky: return GameConstants.INITAL_INKY_POSITION;
                                    case CharacterName.Pinky: return GameConstants.INITIAL_PINKY_POSITION;
                                        case CharacterName.PacMan: return GameConstants.INITIAL_PACMAN_POSITION;
                                            default: return Vector3.zero;
                }        
            }
        }

        [SerializeField]
        private float _timeToStartMove = 2.5F;
        
        private void Start()
        {
            Initialize();
        }

        //Check if the character have the right conditions to walk or not
        private bool CanWalk()
        {
            return (Character.State.ConditionState != CharacterCondition.Freeze && Character.State.ConditionState != CharacterCondition.Dead);
        }

        
        //Happens when 
        private void AuthorizingWalk()
        {
            Character.State.ChangeConditionState(CharacterCondition.Normal);

            ResetDirections();
            ResetState();      
        }

        private void ResetState()
        {
            Character.State.ChangeConditionState(CharacterCondition.Normal);
        }

        private void FixedUpdate()
        {            
            AILogic();            
        }   

        private void Initialize()
        {
            StoreInitialDirections();
            
            if(!CanWalk()) Invoke("AuthorizingWalk", _timeToStartMove);
        }

        private void StoreInitialDirections()
        {
            InitialAllowedDirections = AllowedDirections;
        }

        #region Direction Handlers

        protected bool IsValidDirection(Vector2 direction)
        {
            var characterDirection = CharacterDirectionHelper.GetDirectionByVector(direction);
            
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
        
        private void ResetDirections()
        {
            AllowedDirections = InitialAllowedDirections;
            
       //     if(Character.Name != CharacterName.Blinky)
         //   Character.State.DirectionState = InitialAllowedDirections[0];
        }

        #endregion

        #region Events

        public void OnRespawn()
        {
            Character.State.ChangeConditionState(CharacterCondition.Freeze);
            
            ResetPosition();
                        
            Respawn();
        }
        
        public void OnGameReset()
        {
            Gameplay.Gameplay.ResetPaths();
                
            Character.State.ChangeConditionState(CharacterCondition.Freeze);
            
            ResetDirections();
                        
            ResetPosition();
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

        #endregion

        #region Methods

        public async void Respawn()
        {
            ResetPosition();
            ResetState();
                        
            await Task.Delay(TimeSpan.FromMilliseconds(10));
            
            ResetDirections();
            
            await Task.Delay(TimeSpan.FromSeconds(_timeToStartMove));
                                          
            AuthorizingWalk();
        }
    
        private void ResetPosition()
        {
            transform.position = _startPosition;
        }    

        public void SetVisibility(bool isVisible)
        {
            var sprite = GetComponent<SpriteRenderer>();

            sprite.enabled = isVisible;
        }

        #endregion

        #region Movements

        public void AILogic()
        {
            if(!CanWalk()) return;
            
            var direction = (Vector3) CharacterDirectionHelper.GetVectorByDirectionState(Character.State.DirectionState);

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
                
                Character.State.DirectionState = CharacterDirectionHelper.GetDirectionByVector(desiredPosition);
                                                
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

            Character.State.DirectionState = CharacterDirectionHelper.GetDirectionByVector(choosedDirection);
                                                
            Controller.MoveToPosition(transform.position + (Vector3)choosedDirection);     
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
                
                Character.State.DirectionState = CharacterDirectionHelper.GetDirectionByVector(desiredPosition);
                                                
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

        #endregion  
        
    }
}