using UnityEngine;
namespace RunAndCatch
{
    internal class FinishPlatform : MonoBehaviour
    {
        [SerializeField]
        private Transform _finishStandTransform;       

        [SerializeField]
        private ParticleSystem[] _particleSystems;

        [SerializeField]
        private AudioSource _audioSource;


        private void Start()
        {
            if (_finishStandTransform == null) Debug.LogWarning($"Finish stand transform in {this}, can't be empty"); 
            if (_particleSystems == null) Debug.LogWarning($"particle systems in {this}, can't be empty");          

            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null) Debug.LogWarning($"audio source in {this}, can't be empty");           
        }

        internal Transform FinalLevel()
        {
            _audioSource.Play();
            foreach (var _particleSystem in _particleSystems)
            {
                _particleSystem.Play();
            }
            return _finishStandTransform;
        }
    }
}
