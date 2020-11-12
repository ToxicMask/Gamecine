using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class AutomaticCharacter : MonoBehaviour
    {

        public static int totalCharacters = 0;

        [Range(-1, 1)]
        [SerializeField] int walkDirection = 1;
        [SerializeField] float walkSpeed = 16f;

        // Ground Check
        private float distanceToGround = .1f;
        private float rangeGroundCheck = .1f;
        public float fallDiff = .01f;

        // Gravity
        public float fallVelocity = 1f;

        Rigidbody2D rb = null;


        #region Unity Methods

        private void Awake()
        {
            // Add to Total Characters
            totalCharacters++;

            // Components
            rb = GetComponent<Rigidbody2D>();

            Collider2D collider = GetComponent<Collider2D>();

            if (collider)
            {
                distanceToGround = collider.bounds.extents.y;
                rangeGroundCheck = collider.bounds.extents.x;
            }
        }

        private void Start()
        {
            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted += DestroySelf;
        }

        void FixedUpdate()
        {
            Move();
        }

        private void OnDestroy()
        {
            totalCharacters--;

            // Level Manager
            if (LevelManager.current) LevelManager.current.OnLevelCompleted -= DestroySelf;
        }

        #endregion

        private void Move()
        {
            if (CheckIsGrounded())
            {
                // Walk
                Vector2 linearVelocity = walkDirection * walkSpeed * Vector2.right;

                rb.velocity = (Time.fixedDeltaTime * linearVelocity);
            }

            else
            {
                // Only Verical
                rb.velocity = fallVelocity * Vector3.down;
            }
        }

        private void Stop()
        {
            rb.velocity = Vector2.zero;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }

        bool CheckIsGrounded()
        {
            bool rightTrigger = Physics2D.Raycast(transform.position + (rangeGroundCheck * Vector3.right), Vector3.down, distanceToGround + fallDiff);
            bool leftTrigger = Physics2D.Raycast(transform.position + (rangeGroundCheck * Vector3.left), Vector3.down, distanceToGround + fallDiff);

            return leftTrigger || rightTrigger;
        }

    }
}

