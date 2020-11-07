using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sidescroller.Fighting;
using Sidescroller.Health;
using Sidescroller.Control;
using Sidescroller.Canvas;
using Sidescroller.Music;
using Sidescroller.Status;


namespace Sidescroller.StateMachine
{
    
    public enum MinigameState
    {
        Intro,
        Selection,
        Gameplay,
        EndRound,
        EndBattle,
        EndCutscene,

    }


    public class Fighter2DGameManager : MonoBehaviour
    {
        #region Static Variables
        [HideInInspector]
        public static Fighter2DGameManager current;

        #endregion

        #region Class Variables

        private MinigameState currentState = MinigameState.Intro;

        [Header("Fighter Characters")]
        [Tooltip("Character fighter script. - Left")]
        [SerializeField] SideScrollerFighter fighter1 = null;
        [Tooltip("Character fighter script. - Right")]
        [SerializeField] SideScrollerFighter fighter2 = null;
        [Tooltip("Data pack for AI characters.")]
        [SerializeField] AIFighterData standardProfile = null;

        [Space(2)]

        [Header("Canvas Control")]
        [Tooltip("Introduction canvas script.")]
        [SerializeField] IntroControl introControl = null;
        [Tooltip("Select gameplay mode canvas script.")]
        [SerializeField] SelectionControl selectionControl = null;
        [Tooltip("Battle gameplay canvas script")]
        [SerializeField] GameplayCanvasControl gameControl = null;
        [Tooltip("Conclusion canvas script.")]
        [SerializeField] EndingControl endControl = null;

        [Header("Gameplay UI")]
        [SerializeField] Animator sceneryAnimator = null;
        [SerializeField] GUIControl guiControl = null;
        [SerializeField] GameObject PauseCanvas = null;

        [Header("Audio")]
        [Tooltip("Audio manager script.")]
        [SerializeField] MusicControl levelMusic = null;

        #endregion

        #region Gameplay Variables

        [Header("Battle Variables")]

        [Tooltip("Maximum number of round per combat.")]
        [SerializeField] int maxRound = 3;

        private int roundID = 0;
        private int fighter1Score = 0;
        private int fighter2Score = 0;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Set single instance
            current = this;
        }

        void Start()
        {
            /////////////////////////
            /// Config Game Scene ///
            /////////////////////////

            // Start Intro Sequence
            StartIntroSequence();
        }

        // Check Current Minigamestate
        void Update()
        {
            /// Check Methods realise Methods acording to the current State///
            ProcessCurrentMinigameState();
        }

        #endregion

        #region Process Methods

        void ProcessCurrentMinigameState()
        {
            // Check Current game state in frame
            switch (currentState)
            {

                case (MinigameState.Intro):
                    break;

                case (MinigameState.Gameplay):

                    // Return 

                    int roundDefeatedFighter = CheckForStateFighter(FighterState.Dying);

                    // If result is not neutral, End Round
                    if (roundDefeatedFighter != -1)
                    {
                        int roundVictorID;

                        // Victory for Player 2
                        if (roundDefeatedFighter == 1) roundVictorID = 2;

                        // Victory for Player 1
                        else roundVictorID = 1;

                        // If Round Ends the Battle
                        StartEndRoundSequence(roundVictorID);
                    }
                    break;

                case (MinigameState.EndRound):


                    int roundResult = CheckForStateFighter(FighterState.Dead);

                    // End Sequence when one fighter is in state dead
                    if (roundResult != -1)
                    {

                        if ((roundID == maxRound) || (CheckForWinner() != -1))
                        {
                            // Change Canvas to End Screen / End Game

                            int battleVictorID = -1;

                            if (fighter1Score > fighter2Score) battleVictorID = 1; else battleVictorID = 2;

                            if (battleVictorID != -1) StartEndBattleSequence(battleVictorID);

                        }

                        // Next Round
                        else StartNewRoundSequence();
                    }
                    break;

                case (MinigameState.EndBattle):
                    break;
            }
        }


        #endregion

        #region Start Sequence Methods

        private void StartIntroSequence()
        {
            // Define current state
            currentState = MinigameState.Intro;

            // Change music - Intro music
            levelMusic.IntroMusic();

            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            // Deactivate Pause Menu
            PauseCanvas.SetActive(false);

            // Config Canvas
            ChangeCanvas(currentState);
        }

        public void StartSelectionSequence()
        {
            // Define current state
            currentState = MinigameState.Selection;
            
            // Continue previous Muisic

            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            // Deactivate Pause Menu
            PauseCanvas.SetActive(false);

            // Config Canvas
            ChangeCanvas(currentState);
        } // Public

        public void StartGameplaySequence()
        {

            // Activate Pause Menu
            PauseCanvas.SetActive(true);

            // Reset Round Counter
            roundID = 0;

            // Start New Round
            StartNewRoundSequence();

            //Change to Gameplay canvas
            ChangeCanvas(currentState);
        } // Public

        private void StartNewRoundSequence()
        {

            // Change music - Start music
            levelMusic.PlayStandardMusic();

            // Recover full Health & Position
            ResetAllFightersStats();

            // Activate Player Controllers
            ConfigFighterControllers(true);

            //Update Round Counter & Update UI
            roundID += 1;

            // UI Update
            UpdateRoundDisplay();

            //Update Background
            UpdateBackground();

            // Change State
            currentState = MinigameState.Gameplay;

        }

        private void StartEndRoundSequence(int victorID)
        {
            // Victory to player2
            if (victorID == 1) fighter1Score += 1;
            // Victory to player1
            else if (victorID == 2) fighter2Score += 1;

            // Disable Music
            levelMusic.Stop();

            // Update Display
            UpdateFightersScore();

            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            // Change Current State / Display Victory
            currentState = MinigameState.EndRound;

        }

        private void StartEndBattleSequence(int victorID)
        {

            // Deactivate Pause Menu
            PauseCanvas.SetActive(false);

            // Change To End Song
            levelMusic.PlayEndMusic();


            // Change State
            currentState = MinigameState.EndBattle;

            ChangeCanvas(currentState, victorID);
        }

        #endregion

        #region Action Methods 

        public void ChangeToMainMenuScene()
        {
            SceneManager.LoadScene( (int) AllScenes.MainMenu);
        }

        public void ResetCurrentLevel()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
        }

        public void SetCurrentPlayers(SideScrollerFighter script, int id)
        {

            if (id == 1) fighter1 = script;
            if (id == 2) fighter2 = script;

            if (fighter1 == null || fighter2 == null)
            {
                Debug.Log("ERROR - NO FIGHTER SCRIPT");
            }
        }

        public void SetCurrentPlayers(GameObject player1, GameObject player2)
        {
            fighter1 = player1.GetComponent<SideScrollerFighter>();
            fighter2 = player2.GetComponent<SideScrollerFighter>();

            if (fighter1 == null || fighter2 == null)
            {
                Debug.Log("ERROR - NO FIGHTER SCRIPT");
            }
        }



        private void ConfigFighterControllers(bool controllersEnabled)
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

        public void ConfigFighterControllers(int playerID, ControllerMode mode )
        {
            // Get Controller Components

            SideScrollerController controller1 = fighter1.GetComponent<SideScrollerController>();
            SideScrollerController controller2 = fighter2.GetComponent<SideScrollerController>();

            if (controller1 == null || controller2 == null)
            {
                Debug.LogAssertion(" No Controlers Found ");
                return;
            }

            //Config AI Players
            if (mode == ControllerMode.AI_PLAYER)
            {
                if (playerID == 1) controller1.SetCharacterControllerAsAI(standardProfile);
                if (playerID == 2) controller2.SetCharacterControllerAsAI(standardProfile);

                return;
            }

            // Config Local Players
            if (playerID == 1) controller1.currentMode = mode;
            if (playerID == 2) controller2.currentMode = mode;
            return;
        }

        private void ChangeCanvas(MinigameState toCanvas, int victorID = -1)
        {
            switch (toCanvas)
            {
                case (MinigameState.Intro):

                    // Deactivate
                    DeactivateAllCanvas();

                    // Activate
                    introControl.gameObject.SetActive(true);
                    introControl.PlayIntro();

                    break;

                case (MinigameState.Selection):

                    // Deactivate
                    DeactivateAllCanvas();


                    // Activate
                    selectionControl.gameObject.SetActive(true);
                    selectionControl.Display();

                    break;

                case (MinigameState.Gameplay):

                    // Deactivate
                    DeactivateAllCanvas();

                    // Activate
                    gameControl.gameObject.SetActive(true);
                    gameControl.StartGameplay();

                    break;

                case (MinigameState.EndBattle):

                    // Deactivate
                    DeactivateAllCanvas();

                    // Activate
                    endControl.gameObject.SetActive(true);
                    endControl.StartEndingCutscene (victorID);

                    break;
            }
        }

        private void ChangeCurrentMinigameState(MinigameState newState)
        {
            currentState = newState;
        }

        private void DeactivateAllCanvas()
        {

            // End Scripts Methods
            introControl.StopIntro();
            selectionControl.Hide();
            gameControl.StopGameplay();
            endControl.StopEnd();

            // Deactivate Object
            introControl.gameObject.SetActive(false);
            selectionControl.gameObject.SetActive(false);
            gameControl.gameObject.SetActive(false);
            endControl.gameObject.SetActive(false);
        }

        private void ResetAllFightersStats() { // Recover Health and Position of Player

            fighter1.GetComponent<SideScrollerHealth>().FillToMaxHealth();
            fighter2.GetComponent<SideScrollerHealth>().FillToMaxHealth();

            fighter1.ResetFighterPosition();
            fighter2.ResetFighterPosition();

            // Reset for New Animations
            fighter1.EnableAnimationChange(true);
            fighter2.EnableAnimationChange(true);


            fighter1.ChangeState(FighterState.Idle);
            fighter2.ChangeState(FighterState.Idle);
            
        }

        private void UpdateBackground()
        {
            if (roundID == maxRound)
            {
                sceneryAnimator.SetTrigger("Final Stage");
            }
        }

        private void UpdateRoundDisplay()
        {
            // Update Round Conter // Display Final Mark; if is final round
            guiControl.UpdateRoundDisplay(roundID, roundID == maxRound);
        }

        private void UpdateFightersScore()
        {
            //Update fighter Score Counter
            guiControl.UpdateFightersScore(fighter1Score, fighter2Score);
        }

        #endregion

        #region Check Methods

        int CheckForStateFighter(FighterState targetState) /// Return fighter in current state; -1 if no one is found ///
        {
            if (fighter1.currentState == targetState)
            {
                return 1;
            }

            if (fighter2.currentState == targetState)
            {
                return 2;
            }

            /// No Victor ///
            return -1;
        }

        int CheckForWinner()
        {
            int victoryMin = Mathf.CeilToInt((float)maxRound / 2f);

            if (fighter1Score >= victoryMin) return 1;
            if (fighter2Score >= victoryMin) return 2;

            return -1;
        }

        #endregion

        
    }
}
