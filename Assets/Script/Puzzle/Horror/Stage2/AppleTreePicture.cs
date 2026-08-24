using System.Net.NetworkInformation;
using Script.Interactable_Object;
using Script.Interactable_Object.AbstractInteractable;
using Script.Players.Components;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage2
{
    public class AppleTreePicture : RequiringInteractor
    {
        [SerializeField] private InteractableItemSO needItem;
        [SerializeField] private GameObject apple;
        [SerializeField] private Vector3 applePosition;
        [SerializeField] private Texture withApple;
        [SerializeField] private Texture noneApple;
        [SerializeField] private Renderer renderer;
        private bool _isNowTree;
        public override void Interact(InteractManager owner)
        {
            if (_isNowTree)
            {
                Instantiate(apple, applePosition, Quaternion.identity);
                _isNowTree = false;
                renderer.material.mainTexture = noneApple;
                return;
            }
            base.Interact(owner);
            _isNowTree = true;
            renderer.material.mainTexture = withApple;
        }
    }
}