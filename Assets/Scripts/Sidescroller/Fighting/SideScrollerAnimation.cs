using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Fighting;

namespace Sidescroller.Animation
{
    

    public class SideScrollerAnimation : MonoBehaviour
    {

        private Animator animator;

        FighterState currentAnimation;

        public bool canChangeAnimation;

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();

            canChangeAnimation = true;
        }

        public void ChangeAnimationState( FighterState newState)

        {
            // Redundent
            if (currentAnimation == newState) return;

            // Return
            if (!animator.isActiveAndEnabled) return;

            //Expeceptions
            if (!canChangeAnimation) return;

            // Update animator
            currentAnimation = newState;

            PlayAnimation(currentAnimation);

        }

        void PlayAnimation(FighterState state)
        {


            switch (state)
            {

                case FighterState.Idle:
                    animator.Play("Idle");
                    break;

                case FighterState.WalkLeft:
                    animator.Play("Walk Left");
                    break;

                case FighterState.WalkRight:
                    animator.Play("Walk Right");
                    break;

                case FighterState.Attacking:
                    animator.Play("Attack");
                    break;

                case FighterState.Blocking:
                    animator.Play("Block");
                    break;

                case FighterState.Damaged:
                    animator.Play("Get Damaged");
                    break;

                case FighterState.Dying:
                    animator.Play("Dying");
                    break;

            }
        }
    }
}