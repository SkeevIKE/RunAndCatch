using UnityEngine;

namespace RunAndCatch
{
    internal class TriggerWall : MonoBehaviour
    {
        // remove tokens that are in the trigger zone
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Token token))
            {
                token.RemoveToken();
            }
        }
    }
}
