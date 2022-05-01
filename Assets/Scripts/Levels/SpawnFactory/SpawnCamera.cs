using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnCamera : SpawnGameObject <CameraFollow>
    {
        public SpawnCamera(GameObject spawnObject, Transform spawnParent) : base(spawnObject,  spawnParent) { }

        // spawn a camera group
        internal override CameraFollow SpawnAndGetObject()
        {
            CameraFollow characterMotor = SpawnHelper<CameraFollow>.SpawnAndGetComponent(spawnGameObject: this);
            return characterMotor;
        }
    }
}
