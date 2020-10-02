using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Animation;

namespace Sidescroller.Movement
{

    
    public class SideScrollerMover : MonoBehaviour
    {
        Vector2 movementDirection = new Vector2();
        [SerializeField] float walkSpeed = .6f;

        [SerializeField] [Range(-1, 1)] float knockbackDirection = -1f;
        [SerializeField] float knockbackSpeed = .4f;


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
