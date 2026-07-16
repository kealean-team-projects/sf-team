using PrimeTween;
using Script.Players.Components;
using Script.Players.Components.Inputs;
using Script.Players.Interface;
using UnityEngine;

namespace Script.Players {
    public class Player : MonoBehaviour, IPlayerSizeController {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private PlayerInputReader reader;
        [SerializeField] private InteractManager interactor;
        [SerializeField] private float speed;
        [SerializeField] private float jumpPow;
        [SerializeField] private Vector3 cali;
        [SerializeField] private Vector3 middle;
        [SerializeField] private LayerMask whatIsGround;

        private bool _arrowJump;
        private Vector3 _moveDir;

        private void Awake() {
            reader.OnMovePressed += OnMove;
            reader.OnJumpPressed += OnJump;
            reader.OnInteractPressed += OnInteract;
        }

        private void Reset() {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {
            Vector3 moveDirUpdate =  _moveDir.x * transform.right + _moveDir.z * transform.forward;
            var velocity = new Vector3 {
                y = rb.linearVelocity.y,
                x = moveDirUpdate.x * speed,
                z = moveDirUpdate.z * speed
            };
            rb.linearVelocity = velocity;

            var jhit = Physics.OverlapBox(transform.position + cali, middle, Quaternion.identity, whatIsGround);

            _arrowJump = jhit.Length > 0;

            // if (new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).sqrMagnitude < speed) {
            //     rb.AddForce(_moveDir * speed, ForceMode.Force);
            // }
        }


        private void OnDestroy() {
            reader.OnMovePressed -= OnMove;
            reader.OnJumpPressed -= OnJump;
            reader.OnInteractPressed -= OnInteract;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireCube(transform.position + cali, middle);
        }

        private void OnInteract() {
            interactor.Interact();
        }
        
        private void OnJump() {
            if (_arrowJump) rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
        }

        private void OnMove(Vector3 obj) {
            _moveDir = obj;
        }

        public void SizeUp() {
            Tween.Scale(transform, Vector3.one * 3, 0.5f, Ease.OutBack);
        }
        public void SizeDown() {
            Tween.Scale(transform, Vector3.one, 0.5f, Ease.InBack);
        }
    }
}