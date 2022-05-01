using System;
using System.Collections;
using UnityEngine;

namespace RunAndCatch
{
    internal class CharacterMoveToTarget : MonoBehaviour, IMoveToTarget
    { 
        public event Action MoveIsDone;

        private IPlayAnimation _playAnimation;               
        private float _currentYAngel;
        private float _moveTimeCount;
        private float _rotationTimeCount;       
        private float _moveTimeToTarget;
        private float _rotationTimeToTarget;

        void IMoveToTarget.MoveToTarget(Transform target, Character character)
        {
            _playAnimation = character.PlayAnimation;
            _moveTimeToTarget = character.CharacterSettings.MoveTimeToTarget;
            _rotationTimeToTarget = character.CharacterSettings.RotationTimeToTarget;
            StartCoroutine(MoveTo(target));
        }

        private IEnumerator MoveTo(Transform target)
        {
            Quaternion rotationTarget = CalculationRotationTarget(target);
            var heading = target.position - transform.position;
            _playAnimation.AnimationMove(true);

            // move the character towards the target
            _moveTimeCount = 0;
            _rotationTimeCount = 0;
            Vector3 startPosition = transform.position;
            Quaternion startRotation = transform.rotation;
            while (_moveTimeCount < 1)
            {
                heading = target.position - transform.position;
                Move(startPosition, target.position);
                RotationToTarget(startRotation, rotationTarget);                
                yield return null;
            }
            _playAnimation.AnimationMove(false);

            // rotate the character in the given direction
            _rotationTimeCount = 0;
            startRotation = transform.rotation;
            while (_rotationTimeCount < 1)
            {
                RotationToTarget(startRotation, target.rotation);               
                yield return null;
            }

            MoveIsDone?.Invoke();
        }

        private Quaternion CalculationRotationTarget(Transform target)
        {            
            var targetPoint = target.position - transform.position;
            var quaternionTarget = Quaternion.LookRotation(targetPoint, Vector3.up);
            return quaternionTarget;
        }

        private void Move(Vector3 startPosition, Vector3 target)
        {  
            transform.position = Vector3.Lerp(startPosition, target, _moveTimeCount); 
            _moveTimeCount += Time.deltaTime / _moveTimeToTarget;
        }

        private void RotationToTarget(Quaternion startRotation, Quaternion tagetRotation)
        {
            transform.rotation = Quaternion.Slerp(startRotation, tagetRotation, _rotationTimeCount);
            _rotationTimeCount += Time.deltaTime / _rotationTimeToTarget;
        }
    }
}
