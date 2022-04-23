using UnityEngine;

namespace RunAndCatch
{
    internal class TriggerWall : MonoBehaviour
    {
        [SerializeField]
        private string _tokensTag;

        // remove tokens that are in the trigger zone
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == _tokensTag)
            {
                if (other.gameObject.TryGetComponent(out Token token))
                {
                    token.RemoveToken();
                }
            }
        }
    }
}
