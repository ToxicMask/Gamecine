using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Movement
{

    
    public class SideScrollerMover : MonoBehaviour
    {
        Vector2 movementDirection = new Vector2();
        Animator animator;
        CharacterController characterController;
        [SerializeField] float walkSpeed = .6f;
        void Start()
        {
           animator = GetComponent<Animator>();
           characterController = GetComponent<CharacterController>();
        }

        public void Walk(float direction)
        {
            movementDirection.x = direction;
            TurnCharacter(direction);
            Vector2 linearVelocity = movementDirection * walkSpeed;
            characterController.Move(linearVelocity * Time.deltaTime);

        }

        private void TurnCharacter(float direction)//vira a sprite prum lado ou pro outro
        {
            if (direction < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
