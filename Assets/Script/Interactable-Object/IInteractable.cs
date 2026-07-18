using System;
using Script.Players.Components;

namespace Script.Interactable_Object {
    public interface IInteractable {
        void Interact(InteractManager owner);
        void SpecialInteract(InteractManager owner);
    }
}