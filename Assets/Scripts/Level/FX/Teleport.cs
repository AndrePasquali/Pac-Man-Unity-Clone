using DroidDigital.PacMan.Characters.State;
using UnityEngine;

namespace DroidDigital.Level
{
    public class Teleport : MonoBehaviour
    {
        public enum Tunnel
        {
            Left,
            Right
        }

        public Tunnel Type;

        public Transform LeftTunnel;

        public Transform RightTunnel;

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            PerformTeleport(collider2D);
        }

        private void PerformTeleport(Collider2D collider)
        {
            collider.transform.position = Type == Tunnel.Left 
                ? RightTunnel.transform.position + Vector3.left * 2.0F
                : LeftTunnel.transform.position + Vector3.right * 2.0F;

            var desiredDirection = Type == Tunnel.Left ? CharacterDirection.Left : CharacterDirection.Right;

            var characterDirection = collider.gameObject.GetComponent<CharacterState>();
            
            characterDirection.ChangeDirectionState(desiredDirection);
        }
    }
}
