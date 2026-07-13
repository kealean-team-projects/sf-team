using UnityEngine;

namespace Script.Interectable_Object
{
    public class TestInteract : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("interacted");
        }
    }
}