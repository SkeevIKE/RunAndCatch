using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunAndCatch
{
    internal class Character_motor : MonoBehaviour, IControlled
    {
        [SerializeField]
        private float _moveSpeed;

        [SerializeField]
        private float _strafeSpeed;

        [SerializeField]
        private float _rotationSpeed;
        
        private Animator _animator;
        private const int _rotationAngel = 70;
        private const int _multiplierXDeltaRotation = 100;
        private const int _positionZCharacterOffset = 20;
        private const int _borderValue = 4;
        private bool _isRun;       
        private float _currentYAngel;
        private float _currentStrafeDelta;
        private int _animatorRunID;
        private float _oldInputXPosition;
        private Camera _camera;
        
        private void Start()
        {
            _camera = Camera.main;
            _animator = GetComponent<Animator>();
            _animatorRunID = Animator.StringToHash("isRun");
        }

        void IControlled.Run()
        {
            _isRun = !_isRun;
            _animator.SetBool(_animatorRunID, _isRun);
        }

        void IControlled.Strafe(Vector3 inputValue)
        {         
            var inputPoint = new Vector3(inputValue.x, inputValue.y, transform.position.z + _positionZCharacterOffset);
            var inputPosition = _camera.ScreenToWorldPoint(inputPoint);

            float xDelta = inputPosition.x - _oldInputXPosition;
            _oldInputXPosition = inputPosition.x;  
          
            if (_isRun)
            {               
                Vector3 newPosition = new Vector3(Mathf.Clamp(transform.position.x + xDelta * Time.deltaTime * _strafeSpeed, -_borderValue, _borderValue),
                                                  transform.position.y, transform.position.z);
                transform.position = newPosition + Vector3.forward * Time.deltaTime * _moveSpeed;                  
            }
            else
            {
                xDelta = 0;
            }

            RotationCharacter(xDelta);
        }

        private void RotationCharacter(float angelY)
        {
            _currentYAngel = Mathf.LerpAngle(_currentYAngel, angelY, Time.deltaTime * _rotationSpeed);
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  Mathf.Clamp(_currentYAngel * _multiplierXDeltaRotation, -_rotationAngel, _rotationAngel),
                                                  transform.rotation.z);
        }
    }
}