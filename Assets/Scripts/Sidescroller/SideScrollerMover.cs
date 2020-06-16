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

        public void Stand()
        {
            this.GetComponent<Animator>().SetFloat("Walk", 0);
        }


        public void Walk(float direction)
        {

            //Debug.Log(gameObject.name + "Moved !");

            movementDirection.x = direction;
            Vector2 linearVelocity = movementDirection * walkSpeed;
            this.transform.Translate(linearVelocity * Time.deltaTime);

            this.GetComponent<Animator>().SetFloat("Walk", direction);

        }


        
    }
}
