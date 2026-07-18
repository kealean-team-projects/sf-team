using System;
using Script.Players.Components;

namespace Script.Interactable_Object {
    public interface IInteractable {
        void Interact(InteractManager owner); //it is used when player interact this directly
        void SpecialInteract(InteractManager owner); //this one is used when player interact other object but, it needs to act. When you use this, you must do that setItem which method in InteractableItemSO, in Awake and data is 'this'
    }
}