using UnityEngine;
using PoolSpawner;

namespace RunAndCatch
{   
    [RequireComponent (typeof(Character))] [RequireComponent(typeof(AudioSource))]
    internal class FootPrintHandler : MonoBehaviour
    {
        private const float _positionOffset = 0.1f;
        [SerializeField]
        private FootPrint _footPrint;

        [SerializeField]
        private Transform _leftFoot;

        [SerializeField]
        private Transform _rightFoot;

        [SerializeField]
        private FootPrintPool _footPrintPool;

        private Character _character;
        private AudioSource _audioSource;

        private void Start()
        {
            _character = GetComponent<Character>();
            _audioSource = GetComponent<AudioSource>();
            if (_leftFoot == null || _rightFoot == null) Debug.LogWarning($" left Foot or right foot in {this}, can't be empty");            
            if (_footPrintPool == null) Debug.LogWarning($" foot print pool in {this}, can't be empty");
        }

        public void SpawnLeftFootPrint()
        {
            if (_character.IsMove)
            {
                var footPrint = _footPrintPool.TakeObjectFromPool();
                Vector3 spawnPosition = new Vector3(_leftFoot.position.x - _positionOffset, _positionOffset, _leftFoot.position.z);
                footPrint.FootPrinting(spawnPosition);
                _audioSource.PlayOneShot(_character.CharacterSettings.FootStepAudioClip);               
            }
        }

        public void SpawnRightFootPrint()
        {
            if (_character.IsMove)
            {
                var footPrint = _footPrintPool.TakeObjectFromPool();
                Vector3 spawnPosition = new Vector3(_rightFoot.position.x + _positionOffset, _positionOffset, _rightFoot.position.z);
                footPrint.FootPrinting(spawnPosition);
                _audioSource.PlayOneShot(_character.CharacterSettings.FootStepAudioClip);
            }
        }
    }
}
