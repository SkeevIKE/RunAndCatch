using System.Collections;
using UnityEngine;
using PoolSpawner;

namespace RunAndCatch
{
    internal class FootPrint : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem[] _particleSystems;

        internal FootPrintPool FootPrintPool { private get; set; }

        internal void FootPrinting(Vector3 position)
        {
            transform.position = position;            
            transform.SetParent(null);
            gameObject.SetActive(true);

            foreach (var _particleSystem in _particleSystems)
            {
                _particleSystem.Play();
            }
            StartCoroutine(WaitParticleOff());
        }

        IEnumerator WaitParticleOff()
        {
            foreach (var _particleSystem in _particleSystems)
            {               
                yield return new WaitUntil(() => _particleSystem.isStopped);
            }
            ReturnToPool();
        }

        private void ReturnToPool()
        {           
            gameObject.SetActive(false);
            FootPrintPool.PutObjectInPool(this);
        }
    }
}
