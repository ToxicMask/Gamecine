using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GuideCharacter {
    [RequireComponent(typeof(Collider2D))]
    public class Entrance : MonoBehaviour
    {
        public static Entrance current = null;

        // Components
        private Collider2D colliderTrigger = null;

        // Character Prefab
        public GameObject characterPrefab = null;

        // Max Number of Spawn
        public int maxSpawn = 20;
        public int totalSpawned = 0;

        // Time
        public float spawnStartDelay = 2f;
        public float spawnDelay = 1.5f;

        private void Awake()
        {
            current = this;
            colliderTrigger = GetComponent<Collider2D>();
        }

        private void Start()
        {

            // Set Max Spawn
            maxSpawn = LevelManager.current.paulistasIn;

            // Set Invoking
            InvokeRepeating("SpawnCharacter", spawnStartDelay, spawnDelay);

            //Set Events
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += Deactivate;

        }

        private void OnDestroy()
        {
            if (current == this) current = null;
        }

        void SpawnCharacter()
        {

            Debug.Assert(characterPrefab);

            // Max at The Level at the same Type
            if ( totalSpawned >= maxSpawn)
            {
                Deactivate();
                return;
            }
            
            // Spawn Character
            GameObject newCharacter = Instantiate(characterPrefab, transform.position, transform.rotation);
            // Add to Count
            totalSpawned += 1;
        }


        public void Deactivate()
        {
            CancelInvoke();
        }
    }
}
