using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSpawner
{
    public class PoolSpawner <T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected bool _cyclical;                    // selection of the pool operation mode (cyclic mode does not create new objects when the index is equal to the pool size)
        [SerializeField]
        protected int _objectMaxCount;               // pool size      
        private int _index;       
        [SerializeField]
        protected T _spawnTypeObject;        
        private T _typeObject;

        protected T[] _arrayTypes;                   // array of pool objects

        private void Start()
        {
            _arrayTypes = new T[_objectMaxCount];
            while (_index < _objectMaxCount)
            {
                T newObject = CreateObject();
                AttachObjectToPool(newObject);
                _arrayTypes[_index] = newObject;
                _index++;
            }
            _index = 0;            
        }

        // adding objects to the pool
        private void AttachObjectToPool(T setObject)
        {           
            setObject.transform.SetParent(transform);                       
            setObject.gameObject.SetActive(false);                          
        }

        // create objects
        protected virtual T CreateObject() 
        {
            T objectSpawn = Instantiate(_spawnTypeObject);                         
            return objectSpawn;          
        }

        // create an additional object if the pool runs out of objects and the cyclic mode is not selected
        private T CreateAdditionalObject()
        {
           T[] newArrayTypes = new T[_objectMaxCount + 1];
            _arrayTypes.CopyTo(newArrayTypes, 0);
            _arrayTypes = newArrayTypes;

            T newObject = CreateObject();
            AttachObjectToPool(newObject);
            _arrayTypes[_objectMaxCount] = newObject;
            _objectMaxCount++;
            return newObject;
        }

        // take an object from the pool
        public T TakeObjectFromPool()
        {
            _typeObject = null;
            if (!_cyclical)
            {
                for (int i = 0; i < _objectMaxCount; i++)
                {
                    if (_arrayTypes[i] != null)
                    {
                        _typeObject = _arrayTypes[i];
                        _arrayTypes[i] = null;
                        break;
                    }
                }

                if (_typeObject == null)
                {
                    _typeObject = CreateAdditionalObject();
                }
            }
            else
            {  
                if (_index >= _objectMaxCount)                
                {
                    _index = 0;
                }

                if (_arrayTypes[_index] == null)
                {
                    _arrayTypes[_index] = CreateObject();
                }
                _typeObject = _arrayTypes[_index];         
                _typeObject.gameObject.SetActive(false);
                _index++;
            }            
            return _typeObject;                
        }

        // return an object to the pool
        public void PutObjectInPool(T backObject)
        {
            AttachObjectToPool(backObject);
            for (int i = 0; i < _objectMaxCount; i++)
            {
                if (_arrayTypes[i] == null)
                {
                    _arrayTypes[i] = backObject;
                    break;
                }
            }                     
        }
    }
}
