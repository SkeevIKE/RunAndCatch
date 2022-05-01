using System;
using System.Collections;
using UnityEngine;

namespace RunAndCatch
{
    internal class Character : MonoBehaviour
    {
        [SerializeField]
        private CharacterSettings _characterSettings;
        internal CharacterSettings CharacterSettings => _characterSettings;
        internal IPlayAnimation PlayAnimation { get; private set; }

        private IMovement _movement;
        private IInputHandler _inputHandler;

        internal bool IsMove { get; set; }

        internal void Initialization(IInputHandler iInputHandler)
        {
            if (_characterSettings == null) Debug.LogWarning($"character Settings in {this}, can't bo empty");
            
            _inputHandler = iInputHandler;
            SubscribeToInput();

            _movement = new CharacterMovement(transform, _characterSettings);
            PlayAnimation = new CharacterAnimation(animator: GetComponent<Animator>());           
        }

        internal void StopControlCharacter()
        {
            UnsubscribeToInput();          
            IsMove = false;          
        }

        private void SubscribeToInput()
        {
            _inputHandler.LeftMouseButtonDownEvent += SetCharecterIsMovement;
            _inputHandler.MousePositionEvent += CharacterMove;
        }

        private void UnsubscribeToInput()
        {
            _inputHandler.LeftMouseButtonDownEvent -= SetCharecterIsMovement;
            _inputHandler.MousePositionEvent -= CharacterMove;
        }

        private void SetCharecterIsMovement(bool isMove)
        {
            IsMove = isMove;
            PlayAnimation.AnimationMove(IsMove);
        }

        private void CharacterMove(Vector3 position)
        {
            _movement.Move(position, IsMove);
        }
    }
}
