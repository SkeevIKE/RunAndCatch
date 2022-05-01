using System;
using UnityEngine;

namespace RunAndCatch
{
    internal class CharacterMovement : IMovement
    {
        private CharacterSettings _characterSettings;
        private Transform _characterTransform;        
        private const int _rotationAngelRange = 70;
        private const int _multiplierXDeltaRotation = 100;
        private const int _positionZCharacterOffset = 20;
        private const int _borderValue = 4;
        private float _currentYAngel;
        private float _currentStrafeDelta;
        private int _animatorRunID;
        private float _oldInputXPosition;
        
        internal CharacterMovement(Transform transform, CharacterSettings characterSettings)
        {            
            _characterTransform = transform;
            _characterSettings = characterSettings;
        }
        
        void IMovement.Move(Vector3 inputValue, bool isMove)
        {
            float xDelta = 0;
            if (isMove)
            {
                xDelta = inputValue.x - _oldInputXPosition;               
                float newPositionX = Mathf.Clamp(_characterTransform.position.x + xDelta * _characterSettings.StrafeSpeed, -_borderValue, _borderValue);
                Vector3 newPosition = new Vector3(newPositionX, _characterTransform.position.y, _characterTransform.position.z);
                _characterTransform.position = newPosition + Vector3.forward * Time.deltaTime * _characterSettings.MoveSpeed;
            }
            _oldInputXPosition = inputValue.x;
            RotationCharacter(xDelta);
        }
        
        private void RotationCharacter(float angelY)
        {
            _currentYAngel = Mathf.LerpAngle(_currentYAngel, angelY, Time.deltaTime * _characterSettings.RotationSpeed);
            float newRotationY = Mathf.Clamp(_currentYAngel * _multiplierXDeltaRotation, -_rotationAngelRange, _rotationAngelRange);
            _characterTransform.rotation = Quaternion.Euler(_characterTransform.rotation.x, newRotationY, _characterTransform.rotation.z);
        }
    }
}