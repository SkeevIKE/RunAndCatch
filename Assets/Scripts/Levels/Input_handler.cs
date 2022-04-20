using UnityEngine;

namespace RunAndCatch
{
    internal class Input_handler : MonoBehaviour
    {        
        private IControlled _controlled;
       

        internal void Initialisation(IControlled controlled)
        { 
            _controlled = controlled;            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {              
                _controlled.Run();                   
            }                    
            _controlled.Strafe(Input.mousePosition);            
        }
    }
}
