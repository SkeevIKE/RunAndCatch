using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnParticalsBoxes : SpawnGameObject <Transform>
    {        
        private int _levelSize;
        private int _platformSize;

        public SpawnParticalsBoxes(GameObject spawnObject, Transform spawnParent, int levelSize, int platformSize) : base(spawnObject, spawnParent)
        {
            _levelSize = levelSize;
            _platformSize = platformSize;
        }

        // spawn particals boxes in middle level
        internal override void Spawn()
        {
            float positionZ = (_platformSize * (_levelSize / 2)) + _platformSize;
            SpawnPosition = new Vector3(0, 0, SpawnPosition.z + positionZ);

            Transform transform = SpawnHelper<Transform>.SpawnAndGetComponent(spawnGameObject: this);

            var particleSystems = transform.GetComponentsInChildren<ParticleSystem>();
            foreach (var partical in particleSystems)
            {
               int countMaxParticles = partical.main.maxParticles;

                var mainModule = partical.main;
                mainModule.maxParticles = countMaxParticles * _levelSize;

                var shapeModule = partical.shape;
                shapeModule.scale = new Vector3(shapeModule.scale.x, shapeModule.scale.y, _levelSize / 2);
            }
        }
    }
}
