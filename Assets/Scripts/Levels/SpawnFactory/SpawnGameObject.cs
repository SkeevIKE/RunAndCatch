using UnityEngine;

namespace RunAndCatch
{
    internal abstract class SpawnGameObject <T>
    {
        internal GameObject SpawnObject { get; set; }
        internal Vector3 SpawnPosition { get; set; }
        internal Quaternion SpawnRotation { get; set; }
        internal Transform SpawnParent { get; set; }

        internal SpawnGameObject(GameObject spawnObject, Transform spawnParent)
        {
            SpawnObject = spawnObject;
            SpawnPosition = Vector3.zero;
            SpawnRotation = Quaternion.identity;
            SpawnParent = spawnParent;
        }

        internal virtual T[] SpawnAndGetObjects()
        {            
            return default;
        }

        internal virtual T SpawnAndGetObject()
        {
            return default;
        }

        internal virtual void Spawn() { }       
    }
}
