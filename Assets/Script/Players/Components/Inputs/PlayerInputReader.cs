using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Players.Components.Inputs {
    public class PlayerInputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action<Vector3> OnMovePressed;
        public event Action<Vector2> OnMouseMoved; 
        public event Action OnJumpPressed;
        public event Action OnInteractPressed;
        private Controls _controls;

        private void Awake() {
            _controls = new Controls();
            _controls.Enable();
            _controls.Player.SetCallbacks(this);
        }

        private void OnDestroy() {
            _controls?.Dispose();
            _controls = null;
        }

        public void OnMove(InputAction.CallbackContext context) { 
            OnMovePressed?.Invoke(context.ReadValue<Vector3>());
        }
        public void OnLook(InputAction.CallbackContext context) {
            OnMouseMoved?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnInteract(InputAction.CallbackContext context) {
            if (context.performed)
            {
                OnInteractPressed?.Invoke();
            }
        }
        public void OnCrouch(InputAction.CallbackContext context) {
            
        }
        public void OnJump(InputAction.CallbackContext context) {
            if (context.performed) {
                OnJumpPressed?.Invoke();
            }
        }
        public void OnSprint(InputAction.CallbackContext context) {
            
        }
    }
}