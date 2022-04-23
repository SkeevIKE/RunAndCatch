using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnFinishPlatform : SpawnGameObject <FinishPlatform>
    {
        private const string spawnTokenPosition_tag = "SpawnTokenPosition";
        private int _levelSize;
        private int _platformSize;

        public SpawnFinishPlatform(GameObject spawnObject, Transform spawnParent, int levelSize, int platformSize) : base(spawnObject, spawnParent)
        {
            _levelSize = levelSize;
            _platformSize = platformSize;
        }

        // spawn finish platform
        internal override FinishPlatform SpawnAndGetObject()
        {
            float positionZ = _platformSize * _levelSize;
            SpawnPosition = new Vector3(0, 0, SpawnPosition.z + positionZ);
            FinishPlatform finishPlatform = SpawnHelper<FinishPlatform>.SpawnAndGetComponent(spawnGameObject: this);            
            
           return finishPlatform;
        }
    }
}
