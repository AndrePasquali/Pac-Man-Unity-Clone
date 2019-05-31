using Aquiris.PacMan.Characters.State;
using Aquiris.Core.Constants;
using Aquiris.PacMan.Characters.Animation;
using UnityEngine;

namespace Aquiris.PacMan.Characters
{
    [RequireComponent(typeof(Animator))]
    public abstract class Character: MonoBehaviour
    {
        
        #region Components

        public CharacterType Type { get { return Name == CharacterName.PacMan ? CharacterType.Player : CharacterType.AI;}}

        public CharacterName Name;

        public CharacterController MovementCharacterController => _movementCharacterController ?? (_movementCharacterController = GetComponent<CharacterController>());

        private CharacterController _movementCharacterController;

        public Animator Animator => _animator ?? (_animator = GetComponent<Animator>());

        private Animator _animator;

        public CharacterState State => _state ?? (_state = GetComponent<CharacterState>());

        private CharacterState _state;
        
        #endregion

        private void Update()
        {
            EveryFrame();
        }

        protected abstract void EveryFrame();


        protected abstract void ProcessAnimator();

    }
}