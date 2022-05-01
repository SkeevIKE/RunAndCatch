using UnityEngine;
namespace RunAndCatch
{
    internal class CameraFollow : MonoBehaviour
    {        
        private Transform _cameraTarget;

        internal void Initialisation(Transform target)
        {
            _cameraTarget = target;
        }

        private void LateUpdate()
        {
            if (_cameraTarget)
            {
                transform.position = _cameraTarget.position;
            }
        }

    }
}
