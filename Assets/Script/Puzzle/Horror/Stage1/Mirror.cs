using System.Collections.Generic;
using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class Mirror : RequiringInteractor
    {
        [SerializeField] private List<Transform> moveTargets;
        [SerializeField] private Vector3 destination;
        protected override void InteractEffect()
        {
            Debug.Log("Zzang gu rang");
            Vector3 moveValue = destination - gameObject.transform.position;
            foreach (var moveTarget in moveTargets)
            {
                moveTarget.position += moveValue;
            }
        }
    }
}