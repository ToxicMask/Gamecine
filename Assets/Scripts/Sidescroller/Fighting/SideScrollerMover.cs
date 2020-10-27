using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Movement
{

    
    public class SideScrollerMover : MonoBehaviour
    {
        

        // Movement Variables

        // Data
        private float walkSpeed = .6f;
        private float knockbackSpeed = .4f;

        [SerializeField][Range(-1, 1)] float knockbackDirection = -1f;
        Vector2 movementDirection = new Vector2(); // Current Motion


        public void SetStatus(float wSpeed, float kSpeed)
        {
            this.walkSpeed = wSpeed;
            this.knockbackSpeed = kSpeed;
        }

        public void Knockback()
        {
            movementDirection.x = knockbackDirection;

            Vector2 linearVelocity = knockbackSpeed * movementDirection;
            this.transform.Translate(linearVelocity * Time.deltaTime);
        }
        
        public void Walk(float direction)
        {
            //Debug.Log(gameObject.name + "Moved !");
            

            movementDirection.x = direction;
            Vector2 linearVelocity = movementDirection * walkSpeed;
            this.transform.Translate(linearVelocity * Time.deltaTime);
            
        }
    }
}
