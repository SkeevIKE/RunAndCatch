using UnityEngine;

namespace RunAndCatch
{
    internal class Level_status : MonoBehaviour
    {
        [SerializeField]
        private Level_settings levelSettings;
        internal Level_settings LevelSettings => levelSettings;

        private ILevelState _currentState;

        private void Awake()
        {
            ChangeState(new LevelStart_state());
        }

        internal void ChangeState(ILevelState state)
        {
            _currentState = state;
            _currentState.EnterState(levelStatus: this);
        }

    }
}
