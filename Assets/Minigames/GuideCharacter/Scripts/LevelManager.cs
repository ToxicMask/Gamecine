using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace GuideCharacter
{
    public class LevelManager : MonoBehaviour
    {
        // Singletons
        public static LevelManager current = null;

        // End
        private bool levelCompleted = false;

        // Events
        public delegate void LevelEvent();
        public event LevelEvent OnLevelCompleted;

        // Expressions
        private bool InputKeyPress => Input.GetButtonDown("Action Primary") || Input.GetButtonDown("Action Secondary") || Input.GetButtonDown("Pause");

        // Timer
        public Timer levelTimer = null;

        // Canvas
        public Canvas pauseCanvas = null;
        public Canvas gameplayCanvas = null;
        public Canvas endLevelCanvas = null;

        // Text
        public LevelVictoryDisplay endLevelResultText = null;

        // Level State

        [Tooltip("Number of Spawn Paulistas in the Level")]
        public int paulistasIn = 10;
        [Tooltip("Number of  Paulistas that fineshed the Level")]
        public int paulistaOut = 0;
        [Tooltip("Minimal number of OutPaulistas to Win the Level")]
        public int minOut = 5;

        public int score = 0;
        public int scoreBonusPerSecond = 5;


        #region Unity Methods

        private void Awake()
        {
            current = this;
            levelTimer = GetComponent<Timer>();
        }

        private void LateUpdate()
        {
            if (!levelCompleted)
            {
                CheckVictory();
            }
        }

        private void OnDestroy()
        {
            if (LevelManager.current == this) current = null;
        }

        #endregion

        public void ResetCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void CheckVictory()
        {
            // No more Spawns
            bool entranceCleared = Entrance.current.totalSpawned == Entrance.current.maxSpawn;
            int CharacterLeft = AutomaticCharacter.GetCurrentCount();

            if (entranceCleared && (CharacterLeft == 0))
            {
                EndLevel();
            }
            
        }

        public void EndLevel()
        {
            // Make Level Completed
            levelCompleted = true;

            StartCoroutine("LevelCompleted");
        }

        private IEnumerator LevelCompleted()
        {
            // Minimal For Victory
            bool isVictory = (Exit.current.charOut >= minOut);

            // Update Score = Timer if victory
            if (isVictory) score += ((int)levelTimer.TimeLeft) * scoreBonusPerSecond;

            // Perform Action event
            if (OnLevelCompleted != null) OnLevelCompleted();

            // Disable Timer
            if (levelTimer) levelTimer.enabled = false;

            // Disable Pause Canvas
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(false);

            // Disable GUI
            if (gameplayCanvas) gameplayCanvas.gameObject.SetActive(false);

            // Activate Victory Canvas
            if (endLevelCanvas) endLevelCanvas.gameObject.SetActive(true);

            // Update End Level Text
            if (endLevelResultText) endLevelResultText.DisplayResult(isVictory);

            

            yield return new WaitWhile(() => !InputKeyPress);

            // Next Level if victory, else Reload Level
            if (isVictory) SceneManager.LoadScene((int)AllScenes.MainMenu);

            else ResetCurrentLevel();


            yield return null;
        }
    }
}
