using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Movement
{

    
    public class SideScrollerMover : MonoBehaviour
    {
        Vector2 movementDirection = new Vector2();
        Animator animator;
        [SerializeField] float walkSpeed = .6f;
        void Start()
        {
           animator = GetComponent<Animator>();
        }

        public void Walk(float direction)
        {

            Debug.Log(gameObject.name + "Moved 1!");

            movementDirection.x = direction;
            Vector2 linearVelocity = movementDirection * walkSpeed;
            this.transform.Translate(linearVelocity * Time.deltaTime);

        }


        
    }
}
