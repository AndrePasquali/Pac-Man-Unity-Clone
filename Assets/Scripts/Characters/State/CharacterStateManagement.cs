using System;
using System.Collections.Generic;
using System.Linq;
using PathFind;
using UnityEngine;

namespace DroidDigital.PacMan.Characters.State
{    
    internal static class CharacterStateManagement
    {
        public static List<PathDirection> DirectionList = new List<PathDirection>();

        public static void Initialize()
        {
            PopulateDirections();
        }
            
        private static void PopulateDirections()
        {
            foreach (CharacterDirection direction in Enum.GetValues(typeof(CharacterDirection)))
            {
                if(direction == CharacterDirection.Null) continue;          
                DirectionList.Add(new PathDirection
                {
                    IsAlowed = false,
                    Direction = direction
                });
            }
        }
        
        public static CharacterDirection GetDirectionByVector(Vector2 direction)
        {
            if (!DirectionList.Exists(e => e.VectorDirection == direction))
                return CharacterDirection.Null;

            return DirectionList.FirstOrDefault(e => e.VectorDirection == direction).Direction;
        }

        public static Vector2 GetVectorByDirectionState(CharacterDirection direction)
        {                    
            if(direction == CharacterDirection.Null) return Vector2.zero;

            if(DirectionList != null)
                if(DirectionList.Exists(e => e.Direction == direction))
            return DirectionList.FirstOrDefault(e => e.Direction == direction).VectorDirection;
            
            throw new NullReferenceException("Direction List está nulo");
        }

        public static CharacterDirection UpdateStateByMomentSpeed(Vector2 speed)
        {
            if (speed.x > 0) return CharacterDirection.Right;
            if (speed.x < 0) return CharacterDirection.Left;
            if (speed.y > 0) return  CharacterDirection.Up;
            if (speed.y < 0) return CharacterDirection.Down;
            
            return CharacterDirection.Null;
        }
    }
}