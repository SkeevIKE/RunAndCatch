using UnityEngine;

namespace RunAndCatch
{
    internal class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private Level_settings levelSettings;
        internal Level_settings LevelSettings => levelSettings;

        private ILevelStatus _currentStatus;
        internal UIMediator UIMediator { private get; set; }

        private int _playerScores;

        private void Awake()
        {
            ChangeStatus(new LevelBuilder());
            UIMediator.ChangeScoresText(_playerScores);
        }

        internal void ChangeStatus(ILevelStatus status)
        {
            _currentStatus = status;
            _currentStatus.EnterStatus(levelStatus: this);
        }

        internal void AddScoreToPalayer(int value)
        {
            _playerScores += value;
            UIMediator.ChangeScoresText(_playerScores);
        }

    }
}
