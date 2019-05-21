using DroidDigital.PacMan.Characters.State;
using DroidDigital.PacMan.Gameplay;
using UnityEngine;

namespace DroidDigital.Characters
{
    public class CharacterHealth: MonoBehaviour
    {
        public delegate void OnCharacterDie();

        public event OnCharacterDie OnCharacterDieAction;

        public CharacterState CharacterState => _characterState ?? (_characterState = GetComponent<CharacterState>());

        private CharacterState _characterState;

        public AudioClip DeathClip;

        private void Start()
        {
            OnCharacterDieAction += Kill;
        }

        public virtual void Kill()
        {
            CharacterState.ChangeConditionState(CharacterCondition.Dead);
            
            if(DeathClip != null)
                AudioController.Instance.PlayClip(DeathClip);
        }

        private void OnDestroy()
        {
            OnCharacterDieAction -= Kill;
        }
    }
}