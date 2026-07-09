using System;
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
        [SerializeField] private PhysicsShape.ContactFilter whatIsTarget;
        [SerializeField] private bool debug;

        private Vector3 BoxSize => new Vector3(xSize, ySize, zSize);
        private Collider[] _interactArray;

        public void Interact()
        {
            _interactArray = null;
            int count = Physics.OverlapBoxNonAlloc(transform.position, BoxSize*0.5f, _interactArray,
                Quaternion.identity, whatIsTarget.groupIndex);
            if (count <= 0) return;
            for (int i = 0; i < count; i++)
            {
                if (_interactArray != null && _interactArray[i].TryGetComponent<IInteractable>(out var interact))
                {
                    interact.Interact();
                    return;
                }
            }
            
            Debug.LogWarning("감지된 물체중 IInteractable을 가진 물체가 없음");
        }

        private void OnDrawGizmos()
        {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, BoxSize);
        }
    }

    internal interface IInteractable
    {
        void Interact();
    }
}