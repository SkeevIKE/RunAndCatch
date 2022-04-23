using UnityEngine;
using PoolSpawner;

namespace RunAndCatch
{
    internal class FootPrintHandler : MonoBehaviour
    {
        [SerializeField]
        private FootPrint _footPrint;

        [SerializeField]
        private Transform _leftFoot;

        [SerializeField]
        private Transform _rightFoot;

        [SerializeField]
        private AudioClip _footStepAudio;

        [SerializeField]
        private FootPrintPool _footPrintPool;

        private CharacterMotor _characterMotor;
        private AudioSource _audioSource;

        private void Start()
        {            
            if (_leftFoot == null || _rightFoot == null)  
            {
                Debug.LogWarning($" left Foot or right foot in {this}, can't be empty");
            }

            _characterMotor = GetComponent<CharacterMotor>();
            if (_characterMotor == null)
            {
                Debug.LogWarning($" character motor in {this}, can't be empty");
            }

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                Debug.LogWarning($"audio source in {this}, can't be empty");
            }

            if (_footStepAudio == null)
            {
                Debug.LogWarning($" foot step audio in {this}, can't be empty");
            }

            if (_footPrintPool == null)
            {
                Debug.LogWarning($" foot print pool in {this}, can't be empty");
            }
        }

        public void SpawnLeftFootPrint()
        {
            if (_characterMotor.IsRun)
            {
                var footPrint = _footPrintPool.TakeObjectFromPool();
                Vector3 spawnPosition = new Vector3(_leftFoot.position.x - 0.1f, 0.1f, _leftFoot.position.z);
                footPrint.FootPrinting(spawnPosition, _leftFoot.rotation);
                _audioSource.PlayOneShot(_footStepAudio);               
            }
        }

        public void SpawnRightFootPrint()
        {
            if (_characterMotor.IsRun)
            {
                var footPrint = _footPrintPool.TakeObjectFromPool();
                Vector3 spawnPosition = new Vector3(_rightFoot.position.x + 0.1f, 0.1f, _rightFoot.position.z);
                footPrint.FootPrinting(spawnPosition, _rightFoot.rotation);
                _audioSource.PlayOneShot(_footStepAudio);
            }
        }
    }
}
