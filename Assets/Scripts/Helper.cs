using System;
using UnityEngine;

namespace RunAndCatch
{
    internal interface IInputHandler
    {
        event Action<bool> LeftMouseButtonDownEvent;
        event Action<Vector3> MousePositionEvent;
    }

    internal interface IMovement
    {      
        void Move(Vector3 inputValue, bool isMove);
    }

    internal interface IPlayAnimation
    {
        void AnimationMove(bool isMove);       
    }

    internal interface IMoveToTarget
    {
        event Action MoveIsDone;
        void MoveToTarget(Transform target, Character character);        
    }

    internal interface ILevelStatus
    {
        void EnterStatus(Level level);       
    }

    internal interface ILevelStatusUpdate
    {        
        void UpdateStatus();
    }


    internal class SpawnHelper <T> where T : Component
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
