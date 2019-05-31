using UnityEngine;

namespace Aquiris.PacMan.Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        //Speed of the Enemy
        public float Speed;

        //Color of the Enemy
        public Color Color;

        //Difficulty
        public Difficulty Difficulty;       
    }
}
