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
            ChangeClass(ClassName.Pedestrian);
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

        private void FixedUpdate()
        {
            currentClass.FixedLoop();
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            currentClass.OnTriggerEnter(collider);
        }

        private void OnDestroy()
        {
            totalCharacters--;

            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= DestroySelf;
        }

        #endregion

        #region Class Control

        private void ChangeClass(ClassName newClassName)
        {

            CharacterClass newClass = null;

            switch (newClassName)
            {
                case ClassName.Pedestrian: newClass = new Pedestrian(moveScript); break;
                case ClassName.Guard: newClass = new Guard(moveScript); break;
            }



            if (newClass != null)
            {
                // Exit Old State
                currentClass.Exit();

                // Change State
                currentClass = newClass;

                // Enter New State
                currentClass.Setup();
            }
        }

        #endregion

        #region Interaction

        public void Interact()
        {
            //Temp
            if (currentClass.name == ClassName.Pedestrian)
            {
                ChangeClass(ClassName.Guard);
            }
            else if (currentClass.name == ClassName.Guard)
            {
                ChangeClass(ClassName.Pedestrian);
            }

        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
        #endregion


    }
}

