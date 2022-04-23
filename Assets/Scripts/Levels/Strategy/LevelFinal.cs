using UnityEngine;

namespace RunAndCatch
{
    public class LevelFinal : ILevelStatus
    {
        private LevelManager _levelManager;

        void ILevelStatus.EnterStatus(LevelManager levelStatus)
        {
            _levelManager = levelStatus;
            Object.Destroy(levelStatus.InputHandler);
            levelStatus.InputHandler = null;                
            ((ICharacterMoveToTarget)_levelManager.CharacterMotor).MoveToTarget(_levelManager.FinishPlatform.FinalLevel());
        }
    }
}


