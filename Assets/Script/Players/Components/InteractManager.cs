using Script.Interectable_Object;
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
        [SerializeField] private int bufferSize;

        [field: SerializeField] public Transform HandPos { get; private set; }

        [SerializeField] private bool showInteractable;
        private Collider[] _interactArray;

        private Vector3 BoxSize => new(xSize, ySize, zSize);
        private Vector3 BoxPos => transform.TransformPoint(new Vector3(xPos, yPos, zPos));


        private void Awake() {
            _interactArray = new Collider[bufferSize];
        }

        private void OnDrawGizmos() {
            if (!debug) return;
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(Vector3.zero + new Vector3(xPos, yPos, zPos), BoxSize);
        }

        [ContextMenu("afs")]
        public void Interact() {
            var count = Physics.OverlapBoxNonAlloc(BoxPos, BoxSize * 0.5f, _interactArray,
                transform.rotation, whatIsTarget);
            Debug.Log($"Overlap count: {count}");
            if (count <= 0) return;
            var distance = float.MaxValue;
            IInteractable closestInteractor = null;
            for (var i = 0; i < count; i++) {
                var collider = _interactArray[i];
                var rb = collider.attachedRigidbody;
                if ((!rb || !rb.TryGetComponent<IInteractable>(out var interact)) &&
                    !collider.TryGetComponent(out interact)) continue;
                var currentDistance = (collider.transform.position - transform.position).sqrMagnitude;
                if (currentDistance >= distance) continue;
                distance = currentDistance;
                closestInteractor = interact;
            }

            Debug.Log(3);

            if (closestInteractor == null) {
                Debug.LogWarning("감지된 물체중 IInteractable을 가진 물체가 없음");
                return;
            }

            Debug.Log(4);
            closestInteractor.Interact(this);
        }
    }
}