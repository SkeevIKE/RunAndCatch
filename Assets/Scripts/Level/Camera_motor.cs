using UnityEngine;
namespace RunAndCatch
{
    internal class Camera_motor : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraTarget;

        private void LateUpdate()
        {
            if (_cameraTarget)
            {
                transform.position = _cameraTarget.position;
            }
        }

    }
}
