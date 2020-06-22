using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sidescroller.Fighting;
using Sidescroller.Control;
using Sidescroller.Canvas;


namespace Sidescroller.StateMachine
{
    public enum MinigameState
    {
        Intro,
        Gameplay,
        End
    }


    public class Fighter2DMinigameStateMachine : MonoBehaviour
    {

        #region Class Variables

        MinigameState currentState;

        [SerializeField] SideScrollerFighter fighter1 = null;
        [SerializeField] SideScrollerFighter fighter2 = null;

        [SerializeField] IntroControl introControl = null;
        [SerializeField] GameplayCanvasControl gameControl = null;
        [SerializeField] EndingControl endControl = null;
        


        #endregion

        #region Unity Methods

        void Start()
        {
            /////////////////////////
            /// Config Game Scene ///
            /////////////////////////

            // Check SerializeField variables
            if (!PublicVariablesAvailable()) return;

            // Start Intro Sequence
            StartIntroSequence();
        }

        // Check Current Minigamestate
        void Update()
        {
            /// Check Methods realise Action Methods acording to the current State///

            // Check State for Conditions And Variables
            CheckCurrentMinigameState();

            // Check For Player Input acording to current State
            CheckCurrentInput();
        }

        #endregion

        #region Meta Methods

        // Check Public Defined Variables to Reassure the Right Assigment
        bool PublicVariablesAvailable()
        {
            // End Function
            if (fighter1 == null || fighter2 == null)
            {
                Debug.LogAssertion("No Fighters Selected !!!");
                return false;
            }

            if (introControl == null || gameControl == null)
            {
                Debug.LogAssertion("No Canvas Selected !!!");
                return false;
            }

            //Dont end Function
            return true;
        }

        #endregion

        #region Action Methods 

        void ConfigFighterControllers(bool controllersEnabled)
        {
            // Get Controller Components

            SideScrollerController controller1 = fighter1.GetComponent<SideScrollerController>();
            SideScrollerController controller2 = fighter2.GetComponent<SideScrollerController>();


            if (controller1 == null || controller2 == null)
            {
                Debug.LogAssertion(" No Controlers Found ");
                return;
            }

            // Deactivate Players for Intro Sequence (bool)

            controller1.enabled = controllersEnabled;
            controller2.enabled = controllersEnabled;

            // Set Fighters to Stand Still
            controller1.SetCharacterToStandStill();
            controller2.SetCharacterToStandStill();
        }

        void ChangeCanvas(MinigameState to, int victorID = -1)
        {
            switch (to)
            {
                case (MinigameState.Intro):

                    // Deactivate
                    gameControl.StopGameplay();
                    endControl.StopEnd();

                    // Activate
                    introControl.PlayIntro();

                    break;

                case (MinigameState.Gameplay):

                    // Deactivate
                    introControl.StopIntro();
                    endControl.StopEnd();

                    // Activate
                    gameControl.StartGameplay();

                    break;

                case (MinigameState.End):

                    // Deactivate
                    introControl.StopIntro();
                    gameControl.StopGameplay();

                    // Activate
                    endControl.StartEnding (victorID);

                    break;
            }
        }

        void ChangeCurrentMinigameState(MinigameState newState)
        {
            currentState = newState;
        }

        void ChangeToMainMenuScene()
        {
            SceneManager.LoadScene("Main Menu");
        }

        void ResetCurrentLevel()
        {
            SceneManager.LoadScene("2D_Fighter");
        }

        #endregion

        #region Check Methods

        int CheckForDefeatedFighter() /// -1: No Victor ; 1: Player 1 win ; 2: Player 2 win ///
        {
            if (fighter1.currentState == FighterState.Dying)
            {
                /// Player 2 win ///
                return 2;
            }

            if (fighter2.currentState == FighterState.Dying)
            {
                /// Player 1 win ///
                return 1;
            }

            /// No Victor ///
            return -1;
        }

        void CheckCurrentMinigameState()
        {
            // Check Current game state in frame
            switch (currentState)
            {

                case (MinigameState.Intro):
                    break;

                case (MinigameState.Gameplay):

                    // Return 

                    int gameCheck = CheckForDefeatedFighter();

                    // If result is not neutral, Start Ending
                    if (gameCheck != -1) StartEndingSequence(gameCheck);
                    break;

                case (MinigameState.End):
                    break;
            }
        }

        void CheckCurrentInput()
        {
            // Check Current game state in frame
            switch (currentState)
            {

                case (MinigameState.Intro):

                    // Check to Game to Start
                    if (Input.GetButtonDown("Submit")) StartGameplaySequence();

                    break;

                case (MinigameState.Gameplay):
                    // Empty
                    break;

                case (MinigameState.End):

                    // Check to Return to Main Menu
                    if (Input.GetButtonDown("Submit"))
                    {
                        // Return to Main Menu
                        ChangeToMainMenuScene();
                    }

                    break;
            }
        }

        #endregion

        #region Start Sequence Methods

        void StartIntroSequence()
        {
            // Define current state
            currentState = MinigameState.Intro;

            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            // Config Canvas
            ChangeCanvas(currentState);
        }

        void StartGameplaySequence()
        {
            // Activate Player Controllers
            ConfigFighterControllers(true);

            // Change State
            currentState = MinigameState.Gameplay;

            //Stop Intro
            ChangeCanvas(currentState);
        }

        void StartEndingSequence(int victorID)
        {

            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            // Change Current State
            currentState = MinigameState.End;

            // Change Canvas
            ChangeCanvas(currentState, victorID);
        }

        #endregion

    }
}
