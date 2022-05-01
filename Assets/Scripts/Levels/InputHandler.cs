using System;
using UnityEngine;

namespace RunAndCatch
{
    internal class InputHandler : MonoBehaviour, IInputHandler
    {
        public event Action<bool> LeftMouseButtonDownEvent;
        public event Action<Vector3> MousePositionEvent;

        private const int positionZOffset = 20;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {                
                LeftMouseButtonDownEvent?.Invoke(true); 
            }
            else if ( Input.GetMouseButtonUp(0))
            {               
                LeftMouseButtonDownEvent?.Invoke(false);
            }                           
                GetMousePosition();
        }


        private void GetMousePosition()
        {
            var mousePosition = Input.mousePosition;
            var inputPoint = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z + positionZOffset);
            var inputPosition = _camera.ScreenToWorldPoint(inputPoint);
           
            MousePositionEvent?.Invoke(inputPosition);
        }

       
    }
}
