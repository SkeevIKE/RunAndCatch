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
