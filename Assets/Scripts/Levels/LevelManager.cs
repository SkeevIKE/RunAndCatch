using UnityEngine;

namespace RunAndCatch
{
    internal class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private LevelSettings levelSettings;
        internal LevelSettings LevelSettings => levelSettings;

        internal UIMediator UIMediator { private get; set; }
        internal InputHandler InputHandler { get; set; }
        internal CharacterMotor CharacterMotor { get; set; }
        internal FinishPlatform FinishPlatform { get; set; }
        internal CameraMotor CameraMotor { get; set; }

        private ILevelStatus _currentStatus;       
        private int _playerScores;

        private void Awake()
        {
            ChangeStatus(new LevelBuilder());
            UIMediator.ChangeScoresText(_playerScores);            
        }

        // spawn platforms
        internal void ChangeStatus(ILevelStatus status)
        {
            _currentStatus = status;
            _currentStatus.EnterStatus(levelStatus: this);
        }

        // add points to the player and update the hood
        internal void AddScoreToPalayer(int value)
        {
            _playerScores += value;
            UIMediator.ChangeScoresText(_playerScores);
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
