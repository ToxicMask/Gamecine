using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GuideCharacter;

namespace GuideCharacter
{
    public class CursorCommandControl : MonoBehaviour
    {
        // Static Instance
        public static CursorCommandControl current = null;


        // Expression
        private bool mainClick => Input.GetButtonDown("Action Primary");
        private bool secClick => Input.GetButtonDown("Action Secondary");

        private float cursorRadius = .04f;

        // Set New Class
        public ClassName selectedClassName = ClassName.Pedestrian;

        protected List<ClassName> classList = new List<ClassName> { ClassName.Pedestrian, ClassName.Guard };

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            // Event
            if (LevelManager.current)  LevelManager.current.OnLevelCompleted += Deactivated;
        }

        void Update()
        {
            if (mainClick)
            {
                Collider2D[] clickedObjects = Physics2D.OverlapCircleAll(transform.position, cursorRadius);

                foreach (Collider2D clicked in clickedObjects)
                {

                    ICursorChangeClass charInteract = clicked.GetComponent<ICursorChangeClass>();
                    if (charInteract != null)
                    {
                        // Only once
                        charInteract.Try2ChangeClass(selectedClassName);
                        return;
                    }



                    ICursorInteractable interact = clicked.GetComponent<ICursorInteractable>();
                    if (interact != null)
                    {
                        // Only Once
                        interact.Interact();
                        return;
                    }
                }
            }

            else if (secClick)
            {
                NextClassSelection();

            }
        }

        void NextClassSelection()
        {
            int currentClassID = classList.IndexOf(selectedClassName);

            int newIndex = (currentClassID + 1) % classList.Count;

            selectedClassName = classList[newIndex];
        }

        private void OnDestroy()
        {
            if (current == this) current = null;

            // Event
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= Deactivated;
        }


        public void Deactivated()
        {
            gameObject.SetActive(false);
        }
    }
}
