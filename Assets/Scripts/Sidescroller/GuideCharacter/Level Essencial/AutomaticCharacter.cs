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
        CharacterMovement moveScript = null;

        // Current Class
        public CharacterClass currentClass = null;



        #region Unity Methods

        private void Awake()
        {
            // Add to Total Characters
            totalCharacters++;

            // Components
            moveScript = GetComponent<CharacterMovement>();

            // Set Standard Class
            ChangeClass();
        }

        private void Start()
        {
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += DestroySelf;
        }

        private void Update()
        {
            currentClass.UpdateLoop();
        }

        void FixedUpdate()
        {
            currentClass.FixedLoop();
        }

        private void OnDestroy()
        {
            totalCharacters--;

            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= DestroySelf;
        }

        #endregion

        #region Class Control

        private void ChangeClass()
        {
            currentClass = new Pedestrian(moveScript);
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

