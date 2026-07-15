using Script.Players.Components.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Camera {
    public class CameraTest : MonoBehaviour {
        [field: SerializeField] private PlayerInputReader input;
        [SerializeField] private Vector2 mouseDelta;
        [SerializeField] private float currentX = 0f;
        [SerializeField] private float currentY = 0f;
        [SerializeField] private float rotationBoundary = 80f;
        [SerializeField] private float sensitivity;

        private bool lockCursor = true;

        private UnityEngine.Camera camera;

        private void Awake()
        {
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width/2f, Screen.height/2f));
            input.OnMouseMoved += CameraInput;
            camera = UnityEngine.Camera.main;
        }

        private void Update() {
            if (Keyboard.current.escapeKey.wasPressedThisFrame) lockCursor = !lockCursor;
#if !UNITY_EDITOR
            LockCursor(lockCursor);
#endif
        }

        private void LateUpdate() {
            if (lockCursor) RotateCam();
        }

        private void RotateCam() {
            currentX += mouseDelta.x * sensitivity;
            currentY -= mouseDelta.y * sensitivity;

            currentX = Mathf.Repeat(currentX, 360f);
            currentY = Mathf.Clamp(currentY, -rotationBoundary, rotationBoundary);

            transform.localRotation = Quaternion.Euler(0f, currentX, 0f);
            camera.transform.localRotation = Quaternion.Euler(currentY, 0f, 0f);
        }

        private void CameraInput(Vector2 mousePos) {
            mouseDelta = mousePos;
        }
#if !UNITY_EDITOR
        private void LockCursor(bool lockCursor)
        {
            switch (lockCursor){
                case true:Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
                case false:Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            }
        }
#endif
    }
}