using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class CharacterMovement : MonoBehaviour
    {
        #region Variables & Components

        // Walking
        [Range(-1, 1)]
        [SerializeField] int walkDirection = 1;
        [SerializeField] float walkSpeed = 16f;

        // Ground Check
        private float distanceToGround = .1f;
        private float rangeGroundCheck = .1f;
        public float fallDiff = .01f;

        // Gravity
        public float fallVelocity = 1f;

        // Components
        Rigidbody2D rb = null;

        #endregion

        #region Unity Methods
        private void Awake()
        {
            // Components
            rb = GetComponent<Rigidbody2D>();


            Collider2D collider = GetComponent<Collider2D>();

            if (collider)
            {
                distanceToGround = collider.bounds.extents.y;
                rangeGroundCheck = collider.bounds.extents.x;
            }
        }
        #endregion

        #region Movement
        public void StandardMove()
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

        public void OnlyFall()
        {
            if (CheckIsGrounded())
            {
                // Stay Still
                Stop();
            }

            else
            {
                // Only Verical
                rb.velocity = fallVelocity * Vector3.down;
            }
        }

        public void Stop()
        {
            rb.velocity = Vector2.zero;
        }

        public void InvertWalkDirection()
        {
            walkDirection *= -1;
        }

        private bool CheckIsGrounded()
        {
            bool rightTrigger = Physics2D.Raycast(transform.position + (rangeGroundCheck * Vector3.right), Vector3.down, distanceToGround + fallDiff);
            bool leftTrigger = Physics2D.Raycast(transform.position + (rangeGroundCheck * Vector3.left), Vector3.down, distanceToGround + fallDiff);

            return leftTrigger || rightTrigger;
        }
        #endregion

    }
}
