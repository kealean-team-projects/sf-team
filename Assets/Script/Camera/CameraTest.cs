using System;
using Script.Players.Components.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Camera
{
    public class CameraTest : MonoBehaviour
    {
        [field: SerializeField] private PlayerInputReader input;
        [SerializeField] private Vector2 mouseDelta;
        [SerializeField] private float currentX;
        [SerializeField] private float currentY;
        [SerializeField] private float rotationBoundary = 80f;
        [SerializeField] private float sensitivity;

        private bool lockCursor = true;

        private UnityEngine.Camera camera;

        private void Awake()
        {
            input.OnMouseMoved += CameraInput;
            camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame) lockCursor = !lockCursor;
            LockCursor(lockCursor);
            
        }

        private void LateUpdate()
        {
            if (lockCursor) RotateCam();
        }

        private void RotateCam()
        {
            currentX += mouseDelta.x * sensitivity;
            currentY += mouseDelta.y * sensitivity;

            currentX = Mathf.Repeat(currentX, 360f);
            
            float newY = Mathf.Clamp(currentY, -rotationBoundary, rotationBoundary);

            transform.localRotation = Quaternion.Euler(0, currentX, 0);
            camera.transform.eulerAngles = new Vector3(-newY, currentX, 0);
        }

        private void CameraInput(Vector2 mousePos)
        {
            mouseDelta = mousePos;
        }

        private void LockCursor(bool lockCursor)
        {
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}