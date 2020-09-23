using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;

namespace Sidescroller.Canvas
{

    public class EndingControl : MonoBehaviour
    {

        [SerializeField] Animator endAnimator = null;
        [SerializeField] Animator endTextAnimator = null;
        [SerializeField] Camera endCamera = null;

        bool isInputEnabled = true;

        private void Update()
        {
            // Return is input not enabled
            if (!isInputEnabled) return;


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


        public void StartEndingCutscene(int victorID)
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            endAnimator.SetTrigger("Play");

            // Branch Ending
            if (victorID != -1) endAnimator.SetInteger("Victor ID", victorID);
            else Debug.LogAssertion("No Victor!");

            // Show Text
            endTextAnimator.SetTrigger("FadeIn");

            // Config Camera
            endCamera.enabled = true;
        }

        public void StopEnd()
        {
            if (!PublicVariablesAvailable()) return;

            endAnimator.SetTrigger("Stop");
            endCamera.enabled = false;
        }

        public void SkipEnd()
        {
            endAnimator.SetTrigger("Stop");
        }

        bool PublicVariablesAvailable()
        {
            if (endAnimator == null) return false;
            if (endCamera == null) return false;

            return true;
        }
    }
}
