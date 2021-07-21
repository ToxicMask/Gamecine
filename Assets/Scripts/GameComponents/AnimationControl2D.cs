using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameComponents
{
    public class AnimationControl2D : MonoBehaviour
    {

        Animator animator = null;

        string currentState = "";

        void Start()
        {
           animator = GetComponent<Animator>();
        }

        // Update Animation State
        public void ChangeState(string newState)
        {
            // Check Current State
            if (currentState == newState) return;

            // Switch State
            if (animator) animator.Play(newState);
            else print("NO ANIMATOR");

            // Update Animation Script
            currentState = newState;
        }


        public float GetStateLenght()
        {
            return animator.GetCurrentAnimatorStateInfo(0).length;
        }
        
    }
}
