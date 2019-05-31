using Aquiris.PacMan.Characters.State;
using UnityEngine;

namespace PathFind
{
    public class PathDirection
    {
        public bool IsAlowed;

        public CharacterDirection Direction;

        public Vector2 VectorDirection
        {
            get 
            {
                
                switch (Direction)
                {
                        case CharacterDirection.Up: return Vector2.up;
                            case CharacterDirection.Down: return -Vector2.up;
                                case CharacterDirection.Left: return -Vector2.right;
                                    case CharacterDirection.Right: return Vector2.right;
                                        default: return Vector2.right;
                }
            }
        }
    }
}