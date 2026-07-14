using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object.AbstractInteractable {
    public abstract class CanPickupObject : MonoBehaviour, IInteractable {
        private Transform _handPos;
        private bool _isInHand;

        private void FixedUpdate() {
            if (!_isInHand) return;
            transform.position = _handPos.position;
        }

        public void Interact(InteractManager owner) {
            if (_isInHand) {
                _isInHand = false;
                return;
            }

            _handPos = owner.HandPos;
            _isInHand = true;
        }
    }
}