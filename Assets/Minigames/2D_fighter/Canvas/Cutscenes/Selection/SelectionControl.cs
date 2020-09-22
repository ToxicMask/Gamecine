using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Canvas
{
    public class SelectionControl : MonoBehaviour
    {
        //[SerializeField] Animator animator = null;
        [SerializeField] Camera canvasCamera = null;


        public void Display()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config Animator
            //animator.SetTrigger("Play");

            // Config Camera
            canvasCamera.enabled = true;
        }

        public void Hide()
        {
            if (!PublicVariablesAvailable()) return;

            //introAnimator.SetTrigger("Stop");
            canvasCamera.enabled = false;
        }

        bool PublicVariablesAvailable()
        {
            //if (animator == null) return false;
            if (canvasCamera == null) return false;

            return true;
        }
    }
}
