using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GuideCharacter {
    [RequireComponent(typeof(Collider2D))]
    public class Exit : MonoBehaviour
    {
        // Components
        Collider2D colliderTrigger = null;

        // Counter
        int charPassed = 0;
        int minimalPassed = 5;

        private void Awake()
        {
            colliderTrigger = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            
            if (collider.GetComponent<AutomaticCharacter>())
            {
                Destroy(collider.gameObject);
                charPassed++;
            }

            if (charPassed >= minimalPassed)
            {
                LevelManager.current.LevelEnd(true);
            }
        }
    }
}
