using Script.Players.Components;
using UnityEngine;

namespace Script.Interactable_Object.AbstractInteractable
{
    public abstract class RequiringInteractor : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public InteractableItemSO NeedItem { get; private set;}

        protected InteractManager Owner;

        public void Interact(InteractManager owner)
        {
            if (owner.InHandItem != NeedItem) return;
            Owner = owner;
            InteractEffect();
        }

        public void SpecialInteract(InteractManager owner)
        {
            Owner = owner;
            SpecialInteractEffect();
        }

        protected virtual void InteractEffect()
        {
            
        }

        protected virtual void SpecialInteractEffect()
        {
            
        }
    }
}