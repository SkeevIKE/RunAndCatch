using UnityEngine;

namespace RunAndCatch
{    
    internal interface IControlled
    {
        void Run();
        void Strafe(Vector3 inputValue);        
    }
}
