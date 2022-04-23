using UnityEngine;

namespace RunAndCatch
{
    internal interface IControlled
    {
        void Move();
        void Strafe(Vector3 inputValue);
    }

    internal interface ICharacterMoveToTarget
    {
        void MoveToTarget(Transform target);        
    }

    internal interface ILevelStatus
    {
        void EnterStatus(LevelManager levelStatus);       
    }

    internal interface ILevelStatusUpdate
    {        
        void UpdateStatus();
    }

    internal class SpawnHelper <T>
    {
        internal static T SpawnAndGetComponent(SpawnGameObject<T> spawnGameObject)
        {
            if (spawnGameObject == null)
            {
               Debug.LogWarning($"SpawnArgument in class SpawnPrefab is empty, object cannot be spawned");            
            }

            T type  = Object.Instantiate(spawnGameObject.SpawnObject, spawnGameObject.SpawnPosition, spawnGameObject.SpawnRotation, spawnGameObject.SpawnParent).GetComponent<T>();

            if (type == null)
            {
                Debug.LogWarning($"SpawnAndGetComponent method? component could not be found");
            }

           return type;                
        }
    }
}
