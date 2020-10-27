using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;

namespace Sidescroller.Canvas
{

    public class EndingControl : MonoBehaviour
    {

        [SerializeField] Animator endImageAnimator = null;
        [SerializeField] Animator endTextAnimator = null;
        [SerializeField] Camera endCamera = null;

        public bool isInputEnabled = false;

        private void Update()
        {
            // Check if is input not enabled
            if (!isInputEnabled)
            {

                if (Input.GetButtonDown("Submit"))
                {

                    SkipEnd();
                    ActivateInput();
                }
                return;
            }


            // Check to Return to Main Menu
            if (Input.GetButtonDown("Cancel"))
            {
                // Return to Main Menu
                Fighter2DMinigameStateMachine.current.ChangeToMainMenuScene();
            }

            // Reset Minigame
            if (Input.GetButtonDown("Submit"))
            {
                Fighter2DMinigameStateMachine.current.ResetCurrentLevel();
            }
        }

        public void ActivateInput()
        {
            isInputEnabled = true;
        }

        public void StartEndingCutscene(int victorID)
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // AM - Victory
            if (victorID == 1) endImageAnimator.Play("AM Victory");
            // JBB - Victory
            else if (victorID == 2) endImageAnimator.Play("JBB Victory");
            // No Winner
            else Debug.LogAssertion("No Victor!");

            // Config Camera
            endCamera.enabled = true;

            // Show Text - Delay
            Invoke("FadeInText", 7.5f);
            //endTextAnimator.SetTrigger("FadeIn");

            //ActivateInput
            Invoke("ActivateInput", 8f);


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
