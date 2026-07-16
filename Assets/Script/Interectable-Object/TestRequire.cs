using Script.Interectable_Object.AbstractInteractable;
using Script.Players.Components;
using UnityEngine;

namespace Script.Interectable_Object
{
    public class TestRequire : RequiringInteractor
    {
        protected override void InteractEffect()
        {
            Debug.Log("RightItem");
        }
    }
}