using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class Hammer : CanPickupInteractor
    {
        protected override void PickUpEffect()
        {
            _rb.constraints = RigidbodyConstraints.None;
        }
    }
}