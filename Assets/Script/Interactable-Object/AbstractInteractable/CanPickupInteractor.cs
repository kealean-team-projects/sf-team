using Script.Players.Components;
using UnityEngine;

namespace Script.Interactable_Object.AbstractInteractable
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CanPickupInteractor : MonoBehaviour, IInteractable
    {
        [field : SerializeField] public InteractableItemSO Item { get; private set; }

        protected InteractManager Owner;
        private Rigidbody _rb;
        private Transform _handPos;
        private bool _isInHand;

        protected virtual void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Item.SetItem(this);
        }
        protected virtual void FixedUpdate() {
            if (!_isInHand) return;
            transform.position = _handPos.position;
        }

        public virtual void Interact(InteractManager owner)
        {
            Owner = owner;
            if (_isInHand) {
                _isInHand = false;
                _rb.useGravity = true;
                owner.RemoveHandlingItem();
                gameObject.layer = LayerMask.NameToLayer("Interactable");
                return;
            }

            _handPos = owner.HandPos;
            owner.SetHandItem(Item);
            _rb.useGravity = false;
            _isInHand = true;
            gameObject.layer = LayerMask.NameToLayer("UnInteractable");
        }

        public void SpecialInteract(InteractManager owner)
        {
            Owner = owner;
            SpecialInteractEffect();
        }

        protected virtual void SpecialInteractEffect()
        {
        }
    }
}