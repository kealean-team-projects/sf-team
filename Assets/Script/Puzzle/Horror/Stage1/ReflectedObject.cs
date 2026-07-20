using UnityEngine;

namespace Script.Puzzle.Horror.Stage1
{
    public class ReflectedObject : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void FixedUpdate()
        {
            transform.localPosition =
                new Vector3(-target.localPosition.x, target.localPosition.y, -target.localPosition.z);
        }
    }
}