using Script.Interactable_Object;
using Script.Players.Components;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage2
{
    public class FirstPicture : MonoBehaviour, IInteractable
    {

        [SerializeField] private Renderer targetPicture;
        [SerializeField] private Texture afterImage;
        [SerializeField] private Transform flashLight;
        [SerializeField] private Light light;
        private bool _isInteracted;
        public void Interact(InteractManager owner)
        {
            if (_isInteracted) return;
            light.enabled = false;
            targetPicture.material.mainTexture = afterImage;
            _isInteracted = true;
        }

        public void SpecialInteract(InteractManager owner)
        {
        }
    }
}