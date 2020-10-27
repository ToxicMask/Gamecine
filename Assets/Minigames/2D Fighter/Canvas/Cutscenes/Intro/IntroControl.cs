using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;
using TMPro;

namespace Sidescroller.Canvas
{

    public class IntroControl : MonoBehaviour
    {

        [SerializeField] Animator introAnimator = null;
        [SerializeField] Camera introCamera = null;
        [SerializeField] DialogueManager dialogueManager = null;

        private void Awake()
        {
            // Auto - Get
            dialogueManager = GetComponent<DialogueManager>();
        }

        private void Update()
        {
            // Next Line
            if (Input.GetButtonDown("Submit"))
            {
                //Start if is inactive
                if (!dialogueManager.active) {
                    introAnimator.Play("TextDialogue");
                    dialogueManager.StartIntoText();
                }

                else
                {
                    dialogueManager.NextStep();

                    // Checks if it's Done
                    if (!dialogueManager.active) EndIntro();
                }
                
            }

            //Skip Intro
            else if (Input.GetButtonDown("Cancel")) EndIntro();
        }

        #region Action Methods

        public void PlayIntro()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config Animator
            introAnimator.Play("Intro");

            // Config Camera
            introCamera.enabled = true;
        }

        public void StopIntro()
        {
            if (!PublicVariablesAvailable()) return;

            introCamera.enabled = false;
        }

        public void EndIntro()
        {
            Fighter2DMinigameStateMachine.current.StartSelectionSequence();
        }

        bool PublicVariablesAvailable()
        {
            if (introAnimator == null) return false;
            if (introCamera == null) return false;

            return true;
        }

        #endregion
    }
}
