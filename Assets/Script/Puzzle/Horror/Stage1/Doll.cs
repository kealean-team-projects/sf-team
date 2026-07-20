using System.Collections;
using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class Doll : CanPickupInteractor
    {
        [SerializeField] private GameObject bottomPart;
        [SerializeField] private Mesh after;
        [SerializeField] private Vector3 hangingPos;
        [SerializeField] private Vector3 spawnPos;
        [SerializeField] private float delay;

        private Mesh _myMesh;
        private WaitForSeconds Delay => new WaitForSeconds(delay);

        protected override void Awake()
        {
            base.Awake();
            _myMesh = GetComponent<Mesh>();
        }

        protected override void SpecialInteractEffect()
        {
            Owner.RemoveHandlingItem();
            _isInHand = false;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.position = hangingPos;
            StartCoroutine(DropDelay());
        }

        private IEnumerator DropDelay()
        {
            yield return Delay;
            _myMesh = after;
            GameObject go = Instantiate(bottomPart);
            go.transform.position = spawnPos;

        }
    }
}