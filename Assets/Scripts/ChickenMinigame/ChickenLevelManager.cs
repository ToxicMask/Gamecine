using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Game Manager
namespace ChickenGameplay.GameManager
{
    public class ChickenLevelManager : MonoBehaviour
    {

        public enum GAME_STATE {WAIT, UPDATE, FAILURE, VICTORY}


        // Static Singletone
        public static ChickenLevelManager current = null;

        //Pause Canvas
        public Canvas pauseCanvas = null;



        private void Awake()
        {
            current = this;
        }


        private void OnDestroy()
        {
            if (current == this) current = null;
        }


        // General Level Manager
        
        public void GameOver(GAME_STATE gr)
        {
            if (gr == GAME_STATE.VICTORY) print("PEGOU A GALINHA!");
            else print("FUGIU!");

            Time.timeScale = 0;

            //Deactivate Pause Canvas
            if (pauseCanvas) pauseCanvas.gameObject.SetActive(false);

            StartCoroutine(ResetDelay());

        }


        IEnumerator ResetDelay()
        {
            yield return new WaitForSecondsRealtime(3f);
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
