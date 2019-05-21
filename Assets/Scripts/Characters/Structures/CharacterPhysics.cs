using DroidDigital.Characters.Interfaces;
using UnityEngine;

namespace DroidDigital.Characters
{
    public abstract class CharacterPhysics: MonoBehaviour, IKill
    {
        public CharacterHealth CharacterHealth =>
            _characterHealth ?? (_characterHealth = GetComponent<CharacterHealth>());

        private CharacterHealth _characterHealth;
        
        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            OnCharacterCollider(collider2D);
        }
                      
        public abstract void OnCharacterCollider(Collider2D collider);

        public virtual void Kill(CharacterHealth characterHealth)
        {
            characterHealth.Kill();
        }
    }
}