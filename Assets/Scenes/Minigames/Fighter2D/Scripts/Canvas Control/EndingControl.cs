using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;

namespace Sidescroller.Canvas
{

    public class EndingControl : MonoBehaviour
    {
        //Componets
        [Header("Camera")]
        [SerializeField] Camera endCamera = null;

        [Header("Animators")]
        [Tooltip("Displays ending sprites.")]
        [SerializeField] Animator endImageAnimator = null;
        [Tooltip("Displays ending dialogue text.")]
        [SerializeField] Animator endTextAnimator = null;

        [Header("Dialogue")]
        [SerializeField] DialogueManager dialogueManager = null;
        [Tooltip("Ending 1")]
        [SerializeField] Dialogue augustoEnding = null;
        [Tooltip("Ending 2")]
        [SerializeField] Dialogue joaoEnding = null;

        //Variables
        private bool canSkip = true;
        private bool isTextOver = false;
        private int winnerID = -1;

        //Endings text


        private void Update()
        {
            // Check if Text is Over
            if (!isTextOver)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    // Next Line
                    dialogueManager.NextStep();

                    if (!dialogueManager.active)
                    {
                        //Set Animation 
                        SetWinnerAnimation(false);

                        //Set Text Is Over
                        isTextOver = true;                        
                    }

                }

                else if (Input.GetButtonDown("Cancel"))
                {
                    SkipEnd();
                    DeactivateSkip();

                    isTextOver = true;
                }
                // End Update
                return;
            }


            // Check if is input not enabled
            else if (canSkip)
            {
                if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
                {
                    SkipEnd();
                    DeactivateSkip();
                }
                // End Update
                return;
            }


            // Check to Return to Main Menu
            if (Input.GetButtonDown("Cancel"))
            {
                // Return to Main Menu
                Fighter2DGameManager.current.ChangeToMainMenuScene();
            }

            // Reset Minigame
            else if (Input.GetButtonDown("Submit"))
            {
                Fighter2DGameManager.current.ResetCurrentLevel();
            }
        }

        public void DeactivateSkip()
        {
            canSkip = false;
        }

        public void StartEndingCutscene(int victorID)
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config

            // Winner 
            winnerID = victorID;

            //Set winner Animation
            SetWinnerAnimation(true);

            // Config Camera
            endCamera.enabled = true;
        }

        private void SetWinnerAnimation(bool withText)
        {
            if (withText)
            {
                // AM - Victory
                if (winnerID == 1)
                {
                    endImageAnimator.Play("AM Victory Text");
                    dialogueManager.SetNewDialogue(augustoEnding);
                    
                }
                // JBB - Victory
                else if (winnerID == 2)
                {
                    endImageAnimator.Play("JBB Victory Text");
                    dialogueManager.SetNewDialogue(joaoEnding);
                }
                // No Winner
                else Debug.LogAssertion("No Victor!");

                // Start Dialogue
                dialogueManager.StartIntoText();
            }
            else
            {
                // AM - Victory
                if (winnerID == 1) endImageAnimator.Play("AM Victory");
                // JBB - Victory
                else if (winnerID == 2) endImageAnimator.Play("JBB Victory");
                // No Winner
                else Debug.LogAssertion("No Victor!");

                // Config End Text & Input
                ConfigEndImage();                        
            }
        }

        private void ConfigEndImage()
        {
            // Show Text - Delay
            Invoke("FadeInText", 7.5f);
            //endTextAnimator.SetTrigger("FadeIn");

            //ActivateInput
            Invoke("DeactivateSkip", 8f);
        }

        public void StopEnd()
        {
            if (!PublicVariablesAvailable()) return;

            endImageAnimator.SetTrigger("Stop");
            endCamera.enabled = false;
        }

        private void SkipEnd()
        {
            endImageAnimator.Play("End");
            endTextAnimator.Play("Show");
            CancelInvoke();
        }

        private void FadeInText() {
            endTextAnimator.Play("FadeIn");
        }


        bool PublicVariablesAvailable()
        {
            if (endImageAnimator == null) return false;
            if (endCamera == null) return false;

            return true;
        }
    }
}
