using UnityEngine;

namespace RunAndCatch
{
    internal class LevelProgress : ILevelStatus, ILevelStatusUpdate
    {
        private Level _level;
        private float _startValue;
        private float _finishDistanceValue;
        private Transform _characterTransform;
        private const int halfValue = 2;

        void ILevelStatus.EnterStatus(Level level)
        {
            _level = level;
            _characterTransform = level.Character.transform;
            _startValue = _characterTransform.position.z;
            _finishDistanceValue = (level.LevelSettings.LevelSize * level.LevelSettings.PlatformSize) - (level.LevelSettings.PlatformSize / halfValue);            
        }

        // tracking the distance traveled by the character
        void ILevelStatusUpdate.UpdateStatus()
        {
            float distance = CalculateDistance(_characterTransform.position.z);
            _level.ChangeDistanceToFinish(distance);

            if (distance >= 1)
            {
                _level.ChangeStatus(new LevelFinal());
            }
        }

        private float CalculateDistance(float curentDictanceValue)
        {
            return (curentDictanceValue - _startValue) / _finishDistanceValue;
        }

        
    }
}
