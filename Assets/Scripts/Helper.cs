using System;
using UnityEngine;

namespace RunAndCatch
{
    internal interface IControlled
    {
        void Move(bool isMove);
        void Strafe(Vector3 inputValue);
    }

    internal interface ICharacterMoveToTarget
    {
        event Action MoveDone;
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
        // creates an object and returns a component, the spawner type is passed to the argument with the required component to return
        internal static T SpawnAndGetComponent(SpawnGameObject<T> spawnGameObject)
        {
            if (spawnGameObject == null)
            {
               Debug.LogWarning($"argument spawnGameObject in SpawnHelper.SpawnAndGetComponent is empty, object cannot be spawned");            
            }

            T type  = UnityEngine.Object.Instantiate(spawnGameObject.SpawnObject, spawnGameObject.SpawnPosition, spawnGameObject.SpawnRotation, spawnGameObject.SpawnParent).GetComponent<T>();

            if (type == null)
            {
                Debug.LogWarning($"component could not be found in SpawnHelper.SpawnAndGetComponent");
            }

           return type;                
        }
    }
}
