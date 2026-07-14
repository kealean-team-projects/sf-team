using UnityEngine;

namespace DistantLands {
    public class NoiseSway : MonoBehaviour {
        [Tooltip("Controls the strength of the noise.")]
        public float depth = 1;

        [Tooltip("Controls the distance between peaks of the noise.")]
        public float width = 1;

        [Tooltip("Controls the seed of the noise. Leave 0 for a random seed.")]
        public int seed;

        private Vector3 rot;


        // Start is called before the first frame update
        private void Start() {
            if (seed == 0)
                seed = Random.Range(0, 1000000);
            rot = transform.localEulerAngles;
        }

        // Update is called once per frame
        private void Update() {
            var t = Time.time / width;

            var offset = new Vector3(
                Mathf.PerlinNoise(seed, t) - .5f,
                Mathf.PerlinNoise(seed + 1000, t) - .5f,
                Mathf.PerlinNoise(seed + 2000, t) - .5f);


            transform.localEulerAngles = rot + offset * depth;
        }
    }
}