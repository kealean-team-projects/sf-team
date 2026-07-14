using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object.AbstractInteractable
{
    public abstract class CanPickupObject : MonoBehaviour, IInteractable
    {
        private Rigidbody _rb;
        private Transform _handPos;
        private bool _isInHand;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate() {
            if (!_isInHand) return;
            transform.position = _handPos.position;
        }

        public void Interact(InteractManager owner) {
            if (_isInHand) {
                _isInHand = false;
                _rb.useGravity = true;
                return;
            }

            _handPos = owner.HandPos;
            _rb.useGravity = false;
            _isInHand = true;
        }
    }
}