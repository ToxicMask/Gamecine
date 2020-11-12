using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GuideCharacter {
    [RequireComponent(typeof(Collider2D))]
    public class Entrance : MonoBehaviour
    {
        // Components
        Collider2D colliderTrigger = null;

        // Character Prefab
        public GameObject characterPrefab = null;

        // Time

        private void Awake()
        {
            colliderTrigger = GetComponent<Collider2D>();
        }

        private void Start()
        {
            InvokeRepeating("SpawnCharacter", 2f, 1.5f);
        }

        void SpawnCharacter()
        {

            Debug.Assert(characterPrefab);

            GameObject newCharacter = Instantiate(characterPrefab, transform.position, transform.rotation);

            
        }
    }
}
