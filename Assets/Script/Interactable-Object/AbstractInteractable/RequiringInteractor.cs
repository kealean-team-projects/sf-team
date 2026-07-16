using Script.Players.Components;
using UnityEngine;

namespace Script.Interactable_Object.AbstractInteractable
{
    public abstract class RequiringInteractor : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public InteractableItemSO NeedItem { get; private set;}

        private InteractManager _owner;

        public void Interact(InteractManager owner)
        {
            if (owner.InHandItem != NeedItem) return;
            _owner = owner;
            InteractEffect();
        }

        protected virtual void InteractEffect()
        {
            
        }
    }
}