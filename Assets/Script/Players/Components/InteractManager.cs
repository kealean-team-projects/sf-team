using Script.Interactable_Object;
using Script.Interactable_Object.AbstractInteractable;
using UnityEngine;

namespace Script.Players.Components {
    public class InteractManager : MonoBehaviour {
        [SerializeField] private float xSize;
        [SerializeField] private float ySize;
        [SerializeField] private float zSize;
        [SerializeField] private float xPos;
        [SerializeField] private float yPos;
        [SerializeField] private float zPos;
        [SerializeField] private LayerMask whatIsTarget;
        [SerializeField] private bool debug;
        [SerializeField] private int bufferSize = 5;

        [field: SerializeField] public InteractableItemSO InHandItem { get; private set; }

        [field: SerializeField] public Transform HandPos { get; private set; }

        [field: SerializeField] public Player Player { get; private set; }

        [SerializeField] private bool showInteractable;
        private Collider[] _interactArray;

        private Vector3 BoxSize => new(xSize, ySize, zSize);
        private Vector3 BoxPos => transform.TransformPoint(new Vector3(xPos, yPos, zPos));


        private void Awake() {
            _interactArray = new Collider[bufferSize];
            Player = transform.root.GetComponent<Player>();
        }

        private void OnDrawGizmos() {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero + new Vector3(xPos, yPos, zPos), BoxSize);
        }

        public void Interact() {
            var count = Physics.OverlapBoxNonAlloc(BoxPos, BoxSize * 0.5f, _interactArray,
                transform.rotation, whatIsTarget);
            Debug.Log($"Overlap count: {count}");
            if (count <= 0)
            {
                if(InHandItem != null) InHandItem.Item.Interact(this);
                return;
            }
            var distance = float.MaxValue;
            IInteractable closestInteractor = null;
            RequiringInteractor needInteractor = null;
            for (var i = 0; i < count; i++) {
                var collider = _interactArray[i];
                var rb = collider.attachedRigidbody;
                
                if ((!rb || !rb.TryGetComponent<IInteractable>(out var interact)) &&
                    !collider.TryGetComponent(out interact)) continue;
                if (collider.TryGetComponent<RequiringInteractor>(out var requiringInteractor))
                    needInteractor = requiringInteractor;
                
                var currentDistance = (collider.transform.position - transform.position).sqrMagnitude;
                if (currentDistance >= distance) continue;
                
                distance = currentDistance;
                closestInteractor = interact;
            }


            
            
            if (closestInteractor == null) {
                Debug.LogWarning("There's no Object having IInteractable");
                return;
            }

            if (needInteractor == null && InHandItem != null)
            {
                InHandItem.Item.Interact(this);
                return;
            }
            
            closestInteractor.Interact(this);
        }

        public void SetHandItem(InteractableItemSO data)
        {
            InHandItem = data;
        }

        public void RemoveHandlingItem()
        {
            InHandItem = null;
        }
    }
}