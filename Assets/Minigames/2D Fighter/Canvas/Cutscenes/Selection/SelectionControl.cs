using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;
using Sidescroller.Control;

namespace Sidescroller.Canvas
{
    public class SelectionControl : MonoBehaviour
    {
        //[SerializeField] Animator animator = null;
        [SerializeField] Camera canvasCamera = null;

        [SerializeField] ControllerMode selectedMode = ControllerMode.SINGLE_PLAYER;

        [SerializeField] bool isModeSelected = false;
        [SerializeField] int modeID = 0;

        private void Start()
        {
            isModeSelected = false;
        }


        private void Update()
        {
            if (Input.GetButtonDown("Action Primary")) {

                //Toggle Mode
                modeID = (modeID + 1) % (int) ControllerMode.COUNT;

                print((ControllerMode)modeID);
            }

            if (Input.GetButtonDown("Submit"))
            {
                //If not selected, then select
                if (!isModeSelected)
                {
                    isModeSelected = true;

                    selectedMode = (ControllerMode)modeID;
                    print(selectedMode);
                    return;
                }

                else
                {
                    // Config Controllers
                    ConfigFightersInput(selectedMode);


                    Fighter2DMinigameStateMachine.current.StartGameplaySequence();
                }

                
            }
        }


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

        // Show Mode Selection


        bool PublicVariablesAvailable()
        {
            //if (animator == null) return false;
            if (canvasCamera == null) return false;

            return true;
        }


        void ConfigFightersInput(ControllerMode mode)
        {
            // State Machine
            Fighter2DMinigameStateMachine sm = Fighter2DMinigameStateMachine.current;

            // Single Player
            if (mode == ControllerMode.SINGLE_PLAYER)
            {
                //Fighter1 - Single
                sm.ConfigFighterControllers(1, ControllerMode.SINGLE_PLAYER);

                //Fighter2 - AI
                sm.ConfigFighterControllers(2, ControllerMode.AI_PLAYER);

                return;
            }

            // Multiplayer
            else if (mode == ControllerMode.MULTI_PLAYER)
            {
                //Fighter1 - Multi
                sm.ConfigFighterControllers(1, ControllerMode.MULTI_PLAYER);

                //Fighter2 - Multi
                sm.ConfigFighterControllers(2, ControllerMode.MULTI_PLAYER);
            }

            // Expectator 
            else if (mode == ControllerMode.AI_PLAYER)
            {
                //Fighter1 - Single
                sm.ConfigFighterControllers(1, ControllerMode.AI_PLAYER);

                //Fighter2 - AI
                sm.ConfigFighterControllers(2, ControllerMode.AI_PLAYER);
            }

        }
    }
}
