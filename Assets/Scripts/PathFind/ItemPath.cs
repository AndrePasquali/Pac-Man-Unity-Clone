using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Enemy.IA;
using Aquiris.PacMan.Input;
using Aquiris.PacMan.Characters;
using PathFind;
using UnityEditor;
using UnityEngine;
using CharacterMovement = Aquiris.PacMan.Characters.CharacterMovement;
using Random = UnityEngine.Random;

namespace Aquiris.PacMan.PathFind
{
    [RequireComponent(typeof(SpriteRenderer))]
    [Serializable]
    public class ItemPath: MonoBehaviour
    {
        [Header("SETTINGS")]
        public float RayDistance = 1.5F;

        public float OriginRayDistance = 1.0F;
        
        public Transform PathTransform => transform;
        
        public List<PathDirection> PathDirectionList = new List<PathDirection>();

        //[SerializeField]
        public List<CharacterDirection> AlowedDirections = new List<CharacterDirection>();
        
        //[SerializeField]
        public List<CharacterDirection> _initialAllowedDirections = new List<CharacterDirection>();

        public PathCharacter Character = PathCharacter.Both;

        public float PlayerDistance
        {
            get
            {
                var player = GameObject.FindWithTag(GameConstants.PLAYER_TAG);
                return Vector3.Distance(transform.position, player.transform.position);
            }
        }

        public float ID => PathTransform.position.sqrMagnitude;

        public enum PathType
        {
            WayPoint,
            Home
        }

        public PathType Type = PathType.WayPoint;
        
        public CharacterDirection DirectionToAddAfterRelease;

        [Tooltip("USED ONLY IF THE CHARACTER START INSIDE THE HOME")]
        public float TimeToRelease = 5.0F;

        private bool _isReleased;

        private float _timeCounter;

        public SpriteRenderer Sprite => _sprite ?? (_sprite = GetComponent<SpriteRenderer>());

        private SpriteRenderer _sprite;

        private int _index;

        private int _rayIndex;

        private CharacterDirection _lastDirection;

        public float DistanceToDetectWall = 2.0F;

        private float _lastCollidingTime;

        private CharacterDirection _pickedDirection = new CharacterDirection();

        private float _lastUpdateTime;

        private float _lastUpdateDirectionTime;
        
        private void Start()
        {
            Initialize();
        }    
        
        private void Initialize()
        {
            DisableSprite();
            SetInitialAllowedDirections();
        }

        private void EveryFrame()
        {            
            if (Type == PathType.WayPoint) return;
                if (_timeCounter >= TimeToRelease && !_isReleased)
                    OnReleaseEnemy();
                else _timeCounter += Time.deltaTime;
        }

        private void SetInitialAllowedDirections()
        {            
            _initialAllowedDirections = AlowedDirections;
        }
    
        private void Update()
        {
            EveryFrame();    
        }

        private void OnReleaseEnemy()
        {
            _isReleased = true;
            
            if(Type == PathType.WayPoint) return;         

            if(AlowedDirections.Contains(DirectionToAddAfterRelease)) return;
            
            if(DirectionToAddAfterRelease != null)
            AlowedDirections.Add(DirectionToAddAfterRelease);
        }

        private void ResetCounter()
        {
            _timeCounter = 0;
            _isReleased = false;
        }

        private void ResetAllowedDirection()
        {
            if (Type == PathType.WayPoint)
                AlowedDirections = _initialAllowedDirections;
            else
                if (AlowedDirections.Contains(DirectionToAddAfterRelease))
                    AlowedDirections.RemoveAll(e => e == DirectionToAddAfterRelease);

            //AlowedDirections.RemoveAll(e => e == DirectionToAddAfterRelease);
            //AlowedDirections = _initialAllowedDirections;
        }

        public void OnEnemieRespawn()
        {
            ResetCounter();
            ResetAllowedDirection();
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            var isPlayerColliding = collider.transform.CompareTag(GameConstants.PLAYER_TAG);
            var isEnemieColliding = collider.transform.CompareTag(GameConstants.ENEMY_TAG);

            if (isPlayerColliding && (Character == PathCharacter.Both || Character == PathCharacter.PacMan))
            {         
                var character = collider.transform.gameObject.GetComponent<Characters.CharacterMovement>();
                var input = collider.transform.gameObject.GetComponent<InputController>();

                if (Vector3.Distance(transform.position, collider.bounds.center) <= 0.25F) //0.25F
                {
                    character.UpdateAllowedDirections(AlowedDirections);
                    
                    if (!input.AuthorizingMove)
                        input.AuthorizingMove = true;
                    
                    FixPosition(character.gameObject);                
                }
                else
                {
                    input.AuthorizingMove = false;
                }
            }

            if (isEnemieColliding && Vector3.Distance(transform.position, collider.bounds.center) <= 0.25F && 
                (Character == PathCharacter.Ghosts || Character == PathCharacter.Both))
            {
                if(Time.time - _lastCollidingTime < 0.1F) return;

                _lastCollidingTime = Time.time;

                _pickedDirection = _lastDirection;
                                           
                var character = collider.transform.gameObject.GetComponent<EnemyMovement>();
                            
                character.UpdateAllowedDirections(AlowedDirections);
                
                _pickedDirection = AlowedDirections[Random.Range(0, AlowedDirections.Count)];

                if (AlowedDirections.Count > 1)
                {
                    if (_lastDirection != null)
                    {
                        if ( _pickedDirection == _lastDirection)
                            _pickedDirection = AlowedDirections.FirstOrDefault(e => e != _lastDirection);
                    }
                }              

                character.Character.State.DirectionState = _pickedDirection;

               // var characterName = character.name;
                
               // Debug.Log("PICKED: " + character.Character.State.DirectionState + " " + ID + " NAME: " + characterName);
               // Debug.Log("LAST PICKED: " + _lastDirection + " " + ID + " NAME: " + characterName);
                
                //FixPosition(character.gameObject, character.Character.State.DirectionState);
                FixPosition(character.gameObject);

                
                LastDirectionPick(character.Character.State.DirectionState);                
            }                    
        }


        private void LastDirectionPick(CharacterDirection currentDirection)
        {
            _lastDirection = currentDirection;
        }
         
        private void PopulatePossiblePathDirections()
        {
            foreach (CharacterDirection direction in Enum.GetValues(typeof(CharacterDirection)))
            {
                if(direction == CharacterDirection.Null) continue;          
                PathDirectionList.Add(new PathDirection
                {
                    IsAlowed = false,
                    Direction = direction
                });
            }
        }

        public Vector3 GetCurrentDirection()
        {
            var direction = (Vector3) CharacterDirectionHelper.GetVectorByDirectionState(AlowedDirections[_rayIndex]);

            return direction;
        }


        public void PerformRayCasting()
        {
            StartCoroutine(PerformRayCastingAsync());
        }
        

        public IEnumerator PerformRayCastingAsync()
        {
            
            if(AlowedDirections.Count == 0 || AlowedDirections == null) yield break;
            
            yield return new WaitForEndOfFrame();
            
            if (_rayIndex == AlowedDirections.Count)
                _rayIndex = 0;

            var desiredDirection = GetCurrentDirection();
                                
            var rayorigin = transform.position + desiredDirection * OriginRayDistance;

            var hit = Physics2D.Raycast(rayorigin, desiredDirection * RayDistance); 
           
            if(hit.collider == null) yield break;
                
            var collidingNote = hit.collider.CompareTag(GameConstants.NODE_TAG);
            var collidingPlayer = hit.collider.CompareTag(GameConstants.PLAYER_TAG);

            if (collidingNote && !collidingPlayer)
            {
                //Move to next node for search for player
                var itemPath = hit.collider.gameObject.GetComponent<ItemPath>();
                 
                if(itemPath != null) if(itemPath.ID == ID) yield break;
                    
                Debug.DrawRay(rayorigin, desiredDirection * RayDistance, Color.green, 0.1F);

                itemPath.PerformRayCasting();
            }
           // if(collidingPlayer)
             //   Debug.Log("CATCH THE PLAYER! ");

            _rayIndex++;

        }

        private void DisableSprite()
        {
            Sprite.enabled = false;
        }

        private void FixPosition(GameObject target, CharacterDirection direction)
        {
            var verticalPosition = new Vector3(transform.position.x, target.transform.position.y, 0);
            var horizontalPosition = new Vector3(target.transform.position.x, transform.position.y, 0);
            
            switch (direction)
            {
                    case CharacterDirection.Down: target.transform.position = verticalPosition; break;
                        case CharacterDirection.Left: target.transform.position = horizontalPosition; break;
                            case CharacterDirection.Right: target.transform.position = verticalPosition; break;
                                case CharacterDirection.Up: target.transform.position = verticalPosition; break;
            }
        }
        
        private void FixPosition(GameObject target)
        {
            target.transform.position = transform.position;
        }
    }
}