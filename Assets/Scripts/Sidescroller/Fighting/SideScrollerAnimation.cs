using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Fighting;

namespace Sidescroller.Animation
{
    

    public class SideScrollerAnimation : MonoBehaviour
    {

        private Animator animator;

        public FighterState currentAnimation;

        public bool canChangeAnimation;

        public bool activeLoop = false;

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

                case FighterState.Crouching:
                    animator.Play("Crouch Idle");
                    break;

                case FighterState.Damaged:
                    animator.Play("Get Damaged");
                    break;

                case FighterState.Dying:
                    animator.Play("Dying");
                    break;

            }

        }

        public float GetAnimationLenght(FighterState state)
        {

            // Get All Information
            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

            // Anim Info
            string animName = "";
            float animLength = -1f;

            foreach (AnimationClip clip in clips) {
                switch (state)
                {

                    case FighterState.Idle:
                        animName = ("Idle");
                        break;

                    case FighterState.WalkLeft:
                        animName = ("Walk Left");
                        break;

                    case FighterState.WalkRight:
                        animName = ("Walk Right");
                        break;

                    case FighterState.Attacking:
                        animName = ("Attack");
                        break;

                    case FighterState.Blocking:
                        animName = ("Block");
                        break;

                    case FighterState.Crouching:
                        animName = ("Crouch Idle");
                        break;

                    case FighterState.Damaged:
                        animName = ("Get Damaged");
                        break;

                    case FighterState.Dying:
                        animName = ("Dying");
                        break;

                }
            }

            foreach (AnimationClip clip in clips)
            {
                //print(clip.name);
                if (clip.name.Contains(animName) )
                {
                    animLength = clip.length;
                }
            }

            return animLength; 
        }
    }
}