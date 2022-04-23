using System;
using UnityEngine;

namespace RunAndCatch
{
    internal class Token : MonoBehaviour
    {
        [SerializeField]
        private int _tokenScoresValue;

        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private Animator _animator;

        private bool _isTaken;
        private int _animatorIsTakenID;

        internal event Action<int> TokenIsTakenEvent;


        private void Start()
        {
            if (_tokenScoresValue <= 0)
            {
                Debug.LogWarning($"token scores value in {this}, the value is 0 or less than 0");
            }

            if (_particleSystem == null)
            {
                Debug.LogWarning($"particle system in {this}, can't be empty");
            }

            if (_animator == null)
            {
                Debug.LogWarning($"animator in {this}, can't be empty");
            }

            _animatorIsTakenID = Animator.StringToHash("Taken");
        }

        private void Taken()
        {
            TokenIsTakenEvent?.Invoke(_tokenScoresValue);
            _animator.SetTrigger(_animatorIsTakenID);
            _particleSystem.Play();
            _isTaken = true;
        }

        private void LateUpdate()
        {
            if (_isTaken && _particleSystem.isStopped)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            TokenIsTakenEvent = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            Taken();
        }
    }
}
