using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Canvas
{

    public class GameplayCanvasControl : MonoBehaviour
    {

        [SerializeField] Camera gameplayCamera = null;

        public void StartGameplay()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Activate Camera
            gameplayCamera.enabled = true;
        }

        public void StopGameplay()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Activate Camera
            gameplayCamera.enabled = false;
        }

        bool PublicVariablesAvailable()
        {
            if (gameplayCamera == null) return false;

            return true;
        }
    }
}
