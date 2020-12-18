using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GuideCharacter {
    [RequireComponent(typeof(Collider2D))]
    public class Entrance : MonoBehaviour
    {
        // Components
        private Collider2D colliderTrigger = null;

        // Character Prefab
        public GameObject characterPrefab = null;

        // Max Number
        [SerializeField] bool hasMaxSpawn = true;
        [SerializeField] int maxSpawn = 20;

        // Time
        public float spawnStartDelay = 2f;
        public float spawnDelay = 1.5f;

        private void Awake()
        {
            colliderTrigger = GetComponent<Collider2D>();
        }

        private void Start()
        {
            // Set Invoking
            InvokeRepeating("SpawnCharacter", spawnStartDelay, spawnDelay);

            //Set Events
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += Deactivate;
        }

        void SpawnCharacter()
        {

            Debug.Assert(characterPrefab);

            // Max at The Level at the same Type
            if (hasMaxSpawn)
            {
                if ( GameObject.FindGameObjectsWithTag("Player").Length >= maxSpawn) return;
            }
            
            GameObject newCharacter = Instantiate(characterPrefab, transform.position, transform.rotation);
        }


        public void Deactivate()
        {
            CancelInvoke();
        }
    }
}
