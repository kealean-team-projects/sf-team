using System;
using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class Rope : RequiringInteractor
    {
        [SerializeField] private InteractableItemSO mySO;
        
        
        private bool _didGet;

        private void Awake()
        {
            mySO.SetItem(this);
        }

        protected override void InteractEffect()
        {
            _didGet = true;
        }

        protected override void SpecialInteractEffect()
        {
            if (_didGet)
            {
                Debug.Log("Doll Died");
            }
            else
            {
                Debug.Log("Player Died");
            }
        }
    }
}