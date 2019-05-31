using Aquiris.PacMan.Characters.State;
using Aquiris.PacMan.Gameplay;
using UnityEngine;

namespace Aquiris.Characters
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
                AudioManager.Instance.PlaySound(DeathClip);
        }

        private void OnDestroy()
        {
            OnCharacterDieAction -= Kill;
        }
    }
}