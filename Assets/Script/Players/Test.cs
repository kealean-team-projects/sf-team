using System;
using Script.Players.Components;
using Script.Players.Components.Inputs;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Players {
    public class Test : MonoBehaviour {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerInputReader reader;
        [SerializeField] private InteractManager interactor;
        [SerializeField] private float speed;
        [SerializeField] private float jumpPow;

        private Vector3 _moveDir;
        
        private void Reset() {
            rb = GetComponent<Rigidbody>();
        }

        private void Awake() {
            reader.OnMovePressed += OnMove;
            reader.OnJumpPressed += OnJump;
            reader.OnInteractPressed += OnInteract;
            
        }


        private void OnDestroy() {
            reader.OnMovePressed -= OnMove;
            reader.OnJumpPressed -= OnJump;
            reader.OnInteractPressed -= OnInteract;
        }

        private void OnInteract()
        {
            interactor.Interact();
        }

        private void OnJump() {
            rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }

        private void OnMove(Vector3 obj) {
            _moveDir = obj;
        }

        private void FixedUpdate()
        {
            Vector3 velocity = new Vector3();
            velocity.y = rb.linearVelocity.y;
            velocity.x = _moveDir.x * speed;
            velocity.z = _moveDir.z * speed;
            rb.linearVelocity = velocity;

            // if (new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).sqrMagnitude < speed) {
            //     rb.AddForce(_moveDir * speed, ForceMode.Force);
            // }

        }
    }
}