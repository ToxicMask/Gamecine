using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class AutomaticCharacter : MonoBehaviour, ICursorChangeClass
    {

        // Components
        CharacterMovement moveScript = null;

        // Current Class
        public CharacterClass currentClass = null;



        #region Unity Methods

        private void Awake()
        {

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

            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= DestroySelf;
        }

        #endregion

        #region Class Control

        private void ChangeClass(ClassName newClassName)
        {

            CharacterClass newClass = null;

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();


            switch (newClassName)
            {
                case ClassName.Pedestrian: newClass = new Pedestrian(moveScript, spriteRenderer); break;
                case ClassName.Guard: newClass = new Guard(moveScript, spriteRenderer); break;
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

        public void Try2ChangeClass(ClassName newClass)
        {
            if (currentClass.name == newClass || newClass == ClassName.Null) return;

            else
            {
                ChangeClass(newClass);
            }

        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
        #endregion


    }
}

