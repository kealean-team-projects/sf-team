using System;
using UnityEngine;

namespace Script.Test
{
    public class ReflectionObjectTest : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void FixedUpdate()
        {
            transform.localPosition =
                new Vector3(-target.localPosition.x, target.localPosition.y, -target.localPosition.z);
        }
    }
}