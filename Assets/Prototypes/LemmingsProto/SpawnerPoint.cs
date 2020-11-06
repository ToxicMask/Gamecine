using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Prototypes.Lemmings
{
    public class SpawnerPoint : MonoBehaviour
    {
        public GameObject prefab = null;

        public float delay = 1f;
        public float repeat = 3f;

        public bool limited = false;
        public int maxTimes = 5;
        private int times = 0;


        // Start is called before the first frame update
        void Start()
        {
            if (prefab)
            {
                InvokeRepeating("Spawn", delay, repeat);
            }
        }

        protected virtual void Spawn()
        {
            Instantiate(prefab, transform.position, transform.rotation);

            times++;

            if (times >= maxTimes && limited)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void Spawn(Vector3 newPosition)
        {
            Instantiate(prefab, newPosition, transform.rotation);
        }

        protected virtual void Spawn(GameObject modPrefab)
        {

            Instantiate(modPrefab, transform.position, transform.rotation);
        }

        protected virtual void Spawn(GameObject modPrefab, Vector3 newPosition)
        {

            Instantiate(modPrefab, newPosition, transform.rotation);
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}