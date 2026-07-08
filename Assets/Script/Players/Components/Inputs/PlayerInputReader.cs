using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Players.Components.Inputs {
    public class PlayerInputReader : MonoBehaviour, Controls.IPlayerActions {
        public Vector2 Move { get; private set; }
        public event Action OnJumpPressed;
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
            Move = context.ReadValue<Vector2>();
        }
        public void OnLook(InputAction.CallbackContext context) {
            
        }
        public void OnInteract(InputAction.CallbackContext context) {
            
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