using UnityEngine;

namespace Script.Players {
    public class Test : MonoBehaviour {
        [SerializeField] private Rigidbody rb;

        private void Reset() {
            rb = GetComponent<Rigidbody>();
        }
    }
}