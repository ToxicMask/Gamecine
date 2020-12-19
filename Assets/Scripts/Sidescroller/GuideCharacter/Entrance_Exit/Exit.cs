using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GuideCharacter {

    [RequireComponent(typeof(Collider2D))]
    public class Exit : MonoBehaviour
    {
        // Static
        public static Exit current = null;

        // Components
        Collider2D colliderTrigger = null;

        // Counter
        public int charOut = 0;

        private void Awake()
        {
            current = this;
            colliderTrigger = GetComponent<Collider2D>();
        }

        private void Start()
        {
            charOut = 0;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            
            if (collider.GetComponent<AutomaticCharacter>())
            {
                Destroy(collider.gameObject);
                charOut++;

                // Score 
                LevelManager.current.score += 100;
            }
        }

        private void OnDestroy()
        {
            if (current == this) current = null;
        }
    }
}
