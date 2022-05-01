using System;
using System.Collections;
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

        [SerializeField]
        private string _charcterTag;

        internal event Action<int> TokenIsTakenEvent;

        private int _animatorIsTakenID = Animator.StringToHash("Taken");

        private void Start()
        {
            if (_tokenScoresValue <= 0) Debug.LogWarning($"token scores value in {this}, the value is 0 or less than 0");   
            if (_particleSystem == null) Debug.LogWarning($"particle system in {this}, can't be empty");
            if (_animator == null) Debug.LogWarning($"animator in {this}, can't be empty");
        }

        private void Taken()
        {
            TokenIsTakenEvent?.Invoke(_tokenScoresValue);
            _animator.SetTrigger(_animatorIsTakenID);
            _particleSystem.Play();
            StartCoroutine(WaitParticleOff());
        }

        private IEnumerator WaitParticleOff()
        {
            yield return new WaitUntil(() => _particleSystem.isStopped);
            RemoveToken();
        }

        internal void RemoveToken()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            TokenIsTakenEvent = null;
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (other.TryGetComponent(out Character character))
            {
                Taken();
            }
            
        }
    }
}
