using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class MovingChair : MovingInteractor
    {
        [SerializeField] private InteractableItemSO target;

        protected override void InteractEffect()
        {
            target.Item.SpecialInteract(base.Owner);
        }
    }
}
