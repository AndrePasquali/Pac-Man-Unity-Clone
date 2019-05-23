using DroidDigital.Core.Constants;
using DroidDigital.PacMan.Characters;
using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Enemy.IA;
using DroidDigital.PacMan.PathFind;
using UnityEngine;

namespace DroidDigital.Characters.Enemies
{
    public class EnemyVision: MonoBehaviour
    {
        [Header("AI SETTINGS")] [Range(0, 10)] public float DistanceView = 1.0F;

        [Range(0, 10)] public float OriginRayDistance = 1.0F;

        public float RayFrequency = 0.25F;

        public enum Strategy
        {
            Chase,
            WalkAround,
            Embush
        }

        public Strategy CurrentStrategy = Strategy.WalkAround;
        
        private ItemPath _lastDetectedPath;
        
        private float _lastUpdateTime;
        
        public EnemyCharacter Character => _character ?? (_character = GetComponent<EnemyCharacter>());

        private EnemyCharacter _character;

        public EnemyMovement Movement => _movement ?? (_movement = GetComponent<EnemyMovement>());

        private EnemyMovement _movement;

        private void FixedUpdate()
        {
            EveryFrame();
        }

        private void EveryFrame()
        {
            //ProcessVision();
        }
        
        private void ProcessVision()
        {
            if(Time.time - _lastUpdateTime < RayFrequency) return;

            RecordTime();
            
            var direction = (Vector3)CharacterStateManagement.GetVectorByDirectionState(Character.State.DirectionState);

            var rayOrigin = transform.position + direction * OriginRayDistance;

            var hit = Physics2D.Raycast(rayOrigin, direction * DistanceView);
            
            Debug.DrawRay(rayOrigin, direction * DistanceView, Color.green, 0.1F);
  
            if (hit.collider == null) return;

            var isNode = hit.collider.CompareTag(GameConstants.NODE_TAG);
            var isWall = hit.collider.CompareTag(GameConstants.WALL_TAG);
                     
            if (isNode)
            {
                var path = hit.collider.gameObject.GetComponent<ItemPath>();
                    
                if(_lastDetectedPath != null) if(path.ID == _lastDetectedPath.ID) return;
                    
                StorePath(path);
                                        
                path.PerformRayCasting();
            }       
        }

        private void StorePath(ItemPath path)
        {
            _lastDetectedPath = path;
        }

        private void RecordTime()
        {
            _lastUpdateTime = Time.time;
        }

    }
}