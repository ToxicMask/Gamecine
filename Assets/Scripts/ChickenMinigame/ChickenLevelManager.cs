using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Game Manager
namespace ChickenGameplay.GameManager
{
    public class ChickenLevelManager : MonoBehaviour
    {

        public enum GAME_STATE { WAIT, UPDATE, FAILURE, VICTORY }


        // Static Singletone
        public static ChickenLevelManager instance = null;
        public GAME_STATE currentState = GAME_STATE.WAIT;

        // Time Variables
        [Header("Level Time")]
        public float endgameDelay = 2.1f;

        // Pause Canvas
        public Canvas pauseCanvas = null;



        private void Awake()
        {
            instance = this;
        }


        private void OnDestroy()
        {
            if (instance == this) instance = null;
        }


        // General Level Manager

        public void ChangeState(GAME_STATE newState)
        {
            // Prevent Updates
            if (newState == currentState) return;

            // Update State
            currentState = newState;

            // Check Game Over
            switch (currentState)
            {
                case GAME_STATE.VICTORY:
                case GAME_STATE.FAILURE:
                    GameOver(currentState);
                    break;
            }

        }
        
        private void GameOver(GAME_STATE result)
        {
            if (result == GAME_STATE.VICTORY) print("PEGOU A GALINHA!");
            else print("FUGIU!");

            Time.timeScale = 0;

            //Deactivate Pause Canvas
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(false);

            StartCoroutine(ResetDelay());

        }


        IEnumerator ResetDelay()
        {
            yield return new WaitForSecondsRealtime(endgameDelay);
            Time.timeScale = 1;
            ResetCurrentLevel();
        }


        public void ChangeToMainMenuScene()
        {
            SceneManager.LoadScene((int)AllScenes.MainMenu);
        }

        public void ResetCurrentLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}
