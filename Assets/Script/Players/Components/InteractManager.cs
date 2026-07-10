using System;
using System.Linq;
using Script.Interectable_Object;
using Unity.U2D.Physics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Script.Players.Components
{
    public class InteractManager : MonoBehaviour
    {
        [SerializeField] private float xSize;
        [SerializeField] private float ySize;
        [SerializeField] private float zSize;
        [SerializeField] private float xPos;
        [SerializeField] private float yPos;
        [SerializeField] private float zPos;
        [SerializeField] private LayerMask whatIsTarget;
        [SerializeField] private bool debug;
        [SerializeField] private int bufferSize;
        
        private Vector3 BoxSize => new Vector3(xSize, ySize, zSize);
        private Vector3 BoxPos => transform.position + new Vector3(xPos, yPos, zPos);
        private Collider[] _interactArray;
        

        private void Awake()
        {
            _interactArray = new Collider[bufferSize];
        }

        [ContextMenu("afs")]
        public void Interact()
        {
            int count = Physics.OverlapBoxNonAlloc(BoxPos, BoxSize*0.5f, _interactArray,
                transform.rotation, whatIsTarget);
            Debug.Log($"Overlap count: {count}");
            if (count <= 0) return;
            float distance = float.MaxValue;
            IInteractable closestInteractor = null;
            for (int i = 0; i < count; i++)
            {
                var collider = _interactArray[i];
                var rb = collider.attachedRigidbody;
                if (rb && rb.TryGetComponent<IInteractable>(out var interact) || collider.TryGetComponent(out interact))
                {
                    float currentDistance = (collider.transform.position - transform.position).sqrMagnitude;
                    if (currentDistance >= distance) continue;
                    distance = currentDistance;
                    closestInteractor = interact;
                }
                
            }
            Debug.Log(3);

            if (closestInteractor == null)
            {
                Debug.LogWarning("감지된 물체중 IInteractable을 가진 물체가 없음");
                return;
            }
            Debug.Log(4);
            closestInteractor.Interact();
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero + new Vector3(xPos, yPos, zPos), BoxSize);
        }
        
        
    }
}