using UnityEngine;

namespace RunAndCatch
{
    internal class InputHandler : MonoBehaviour
    {        
        private IControlled _controlled;        

        internal void Initialisation(IControlled controlled)
        { 
            _controlled = controlled;            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {               
                _controlled.Move(isMove: true); 
            }
            else if ( Input.GetMouseButtonUp(0))
            {                
                _controlled.Move(isMove: false);
            }

            _controlled.Strafe(Input.mousePosition);            
        }
    }
}
