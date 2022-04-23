using UnityEngine;

namespace RunAndCatch
{
    internal class LevelProgress : ILevelStatus, ILevelStatusUpdate
    {
        private LevelManager _levelManager;
        private float _startValue;
        private float _finishDistanceValue;
        private Transform _characterTransform;

        void ILevelStatus.EnterStatus(LevelManager levelStatus)
        {
            _levelManager = levelStatus;
            _characterTransform = levelStatus.CharacterMotor.transform;
            _startValue = _characterTransform.position.z;
            _finishDistanceValue = levelStatus.LevelSettings.LevelSize * levelStatus.LevelSettings.PlatformSize - 5;            
        }

        // tracking the distance traveled by the character
        void ILevelStatusUpdate.UpdateStatus()
        {
            float distance = CalculateDistance(_characterTransform.position.z);
            _levelManager.ChangeDistanceToFinish(distance);

            if (distance >= 1)
            {
                _levelManager.ChangeStatus(new LevelFinal());
            }
        }

        private float CalculateDistance(float curentDictanceValue)
        {
            return (curentDictanceValue - _startValue) / _finishDistanceValue;
        }

        
    }
}
