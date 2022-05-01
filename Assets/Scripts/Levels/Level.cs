using UnityEngine;

namespace RunAndCatch
{
    [RequireComponent(typeof (AudioSource))]
    internal class Level : MonoBehaviour
    {
        [SerializeField]
        private LevelSettings levelSettings;
        internal LevelSettings LevelSettings => levelSettings;

        internal UIMediator UIMediator { get; set; }
        internal InputHandler InputHandler { get; set; }
        internal Character Character { get; set; }
        internal FinishPlatform FinishPlatform { get; set; }
        internal CameraFollow CameraMotor { get; set; }

        private ILevelStatus _currentStatus;       
        private int _playerScores;
        private AudioSource _audioSource;

        private void Awake()
        {
            ChangeStatus(new LevelBuilder());
            UIMediator.ChangeScoresText(_playerScores);
            SetupAudioSource();
        }

        private void SetupAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = LevelSettings.MusicAudioClip;
            _audioSource.loop = true;
            _audioSource.Play();
        }

        private void PlayAudio(AudioClip audioClip, float volume)
        {
            _audioSource.PlayOneShot(audioClip, volume);
        }

        internal void ChangeStatus(ILevelStatus status)
        {
            _currentStatus = status;
            _currentStatus.EnterStatus(level: this);
        }

        // add points to the player and update the hood
        internal void AddScoreToPalayer(int value)
        {
            _playerScores += value;
            UIMediator.ChangeScoresText(_playerScores);
            PlayAudio(LevelSettings.TokenAudioClip, levelSettings.TokensAudioClipVolume);           
        }

        // update the rest of the distance
        internal void ChangeDistanceToFinish(float value)
        {
            UIMediator.ChangeDistanceBar(value);
        }

        private void Update()
        {
            if (_currentStatus is ILevelStatusUpdate levelStatusUpdate)
            {
                levelStatusUpdate.UpdateStatus();
            }
        }
    }
}
