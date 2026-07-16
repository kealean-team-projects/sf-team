using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Interactable_Object
{
    public class TestRequire : RequiringInteractor
    {
        protected override void InteractEffect()
        {
            Debug.Log("RightItem");
        }
    }
}