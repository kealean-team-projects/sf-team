using System;
using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class Rope : RequiringInteractor
    {
        [SerializeField] private InteractableItemSO myData;
        
        
        
        private bool _didGet;

        private void Awake()
        {
            myData.SetItem(this);
        }

        protected override void InteractEffect()
        {
            _didGet = true;
            NeedItem.Item.SpecialInteract(Owner);
        }

        protected override void SpecialInteractEffect()
        {
            if (_didGet)
            {
                Debug.Log("Doll Die");
            }
            else
            {
                Debug.Log("Player Die");
            }
        }
    }
}