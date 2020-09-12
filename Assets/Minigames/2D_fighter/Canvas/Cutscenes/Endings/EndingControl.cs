using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Canvas
{

    public class EndingControl : MonoBehaviour
    {

        [SerializeField] Animator endAnimator = null;
        [SerializeField] Camera endCamera = null;

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

            // Config Camera
            endCamera.enabled = true;
        }

        public void StopEnd()
        {
            if (!PublicVariablesAvailable()) return;

            endAnimator.SetTrigger("Stop");
            endCamera.enabled = false;
        }

        bool PublicVariablesAvailable()
        {
            if (endAnimator == null) return false;
            if (endCamera == null) return false;

            return true;
        }
    }
}
