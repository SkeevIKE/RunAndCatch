using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnPlatforms : SpawnGameObject <Transform>
    {
        private int _levelSize;
        private int _platformSize;

        internal SpawnPlatforms(GameObject spawnObject, Transform spawnParent, int levelSize, int platformSize) : base(spawnObject, spawnParent)
        {
            _levelSize = levelSize;
            _platformSize = platformSize;
        }

        // spawn a scene from platforms
        internal override Transform[] SpawnAndGetObjects()
        {
            Transform[] transforms = new Transform[_levelSize];
            for (int i = 0; i < _levelSize; i++)
            {
                transforms[i] = SpawnHelper<Transform>.SpawnAndGetComponent(spawnGameObject: this);

                SpawnPosition = new Vector3(0, 0, SpawnPosition.z + _platformSize);
            }
            return transforms;
        }
    }
}