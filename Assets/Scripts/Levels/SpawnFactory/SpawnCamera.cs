using UnityEngine;

namespace RunAndCatch
{
    internal class SpawnCamera : SpawnGameObject <CameraMotor>
    {
        public SpawnCamera(GameObject spawnObject, Transform spawnParent) : base(spawnObject,  spawnParent) { }

        // spawn a camera group
        internal override CameraMotor SpawnAndGetObject()
        {
            CameraMotor characterMotor = SpawnHelper<CameraMotor>.SpawnAndGetComponent(spawnGameObject: this);
            return characterMotor;
        }
    }
}
