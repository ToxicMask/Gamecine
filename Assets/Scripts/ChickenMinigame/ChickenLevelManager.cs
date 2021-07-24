using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Chicken;
using UnityEngine.SceneManagement;


// Game Manager
namespace ChickenGameplay.GameManager
{

    public enum GAME_STATE { WAIT, UPDATE, FAILURE, VICTORY }

    public class ChickenLevelManager : MonoBehaviour
    {

        // Static Singletone
        public static ChickenLevelManager instance = null;
        public GAME_STATE currentState = GAME_STATE.WAIT;

        // Time Variables
        [Header("Level Time")]
        public float startGameDelay = 1.0f;
        public float endGameDelay = 2.1f;

        // Characters
        [Header("Prefabs")]
        public GameObject playerPrefab = null;
        public GameObject chickenPrefab = null;

        // Transforms - Folders
        [Header("Folders")]
        public Transform characterFolder = null;
        public Transform clueFolder = null;

        [Header("Spawn Positions")]
        public Transform playerSpawn = null;
        public Transform chickenSpawn = null;

        [Header("UI")]
        public Canvas pauseCanvas = null;



        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            NewGame();
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

        private void NewGame()
        {
            // Set Up State
            ChangeState(GAME_STATE.WAIT);

            // Spawn Prefabs
            GameObject newPlayer = GameObject.Instantiate(playerPrefab, characterFolder);
            newPlayer.transform.position = playerSpawn.position;

            GameObject newChicken = GameObject.Instantiate(chickenPrefab, characterFolder);
            newChicken.transform.position = chickenSpawn.position;
            newChicken.GetComponent<RunnerChicken>().eggFolder = clueFolder;

            // Set up Delay
            StartCoroutine(BeginingDelay());
        }
        
        private void GameOver(GAME_STATE result)
        {
            if (result == GAME_STATE.VICTORY) print("PEGOU A GALINHA!");
            else print("FUGIU!");

            //Deactivate Pause Canvas
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(false);

            StartCoroutine(ResetDelay());

        }

        IEnumerator BeginingDelay()
        {
            yield return new WaitForSecondsRealtime(startGameDelay);
            ChangeState(GAME_STATE.UPDATE);
        }

        IEnumerator ResetDelay()
        {
            yield return new WaitForSecondsRealtime(endGameDelay);
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
