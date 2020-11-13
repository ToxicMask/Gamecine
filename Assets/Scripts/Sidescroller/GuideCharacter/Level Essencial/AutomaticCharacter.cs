using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class AutomaticCharacter : MonoBehaviour, ICursorInteractable
    {
        // Static
        public static int totalCharacters = 0;

        // Components
        CharacterMovement moveScipt = null;

        #region Unity Methods

        private void Awake()
        {
            // Add to Total Characters
            totalCharacters++;

            // Components
            moveScipt = GetComponent<CharacterMovement>();
        }

        private void Start()
        {
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += DestroySelf;
        }

        void FixedUpdate()
        {
            moveScipt.Move();
        }

        private void OnDestroy()
        {
            totalCharacters--;

            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= DestroySelf;
        }

        #endregion


        #region Interaction

        public void Interact()
        {
            
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
        #endregion


    }
}

