using Script.Players.Components;
using UnityEngine;

namespace Script.Interactable_Object {
    public class TestInteract : MonoBehaviour, IInteractable {
        public void Interact(InteractManager owner) {
            Debug.Log("interacted");
        }

        public void SpecialInteract(InteractManager owner)
        {
            throw new System.NotImplementedException();
        }
    }
}