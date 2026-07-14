using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object {
    public class TestInteract : MonoBehaviour, IInteractable {
        public void Interact(InteractManager owner) {
            Debug.Log("interacted");
        }
    }
}