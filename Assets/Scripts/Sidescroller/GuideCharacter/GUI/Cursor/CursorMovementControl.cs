using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class CursorMovementControl : MonoBehaviour
    {



        // Components
        public DynamicWorldBoundry cursorLimit = null;

        [Range(0, 8)]
        public float cursorSpeed = 4f;

        private void Start()
        {
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += Deacativate;
        }

        private void Update()
        {
            ProcessInput();
        }

        private void OnDestroy()
        {
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= Deacativate;
        }

        void ProcessInput()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector2 move = (moveX * Vector2.right) + (moveY * Vector2.up);

            transform.position = (Vector2)transform.position + ((move * Time.deltaTime) * cursorSpeed);

            if (cursorLimit) transform.position = cursorLimit.ClampPosition(transform.position);
        }

        Vector2 ClampPosition(Vector2 currentPosition)
        {

            float x = Mathf.Clamp(currentPosition.x, -5f, 5f);
            float y = Mathf.Clamp(currentPosition.y, -5f, 5f);

            return new Vector2(x, y);

        }

        void Deacativate()
        {
            this.enabled = false;
        }
    }
}