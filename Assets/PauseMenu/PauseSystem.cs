using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{

    // Public Static var
    public static bool gameIsPaused = false;

    // Pause Menu
    public GameObject pauseMenuUI;

    // Main Menu ID
    [SerializeField] string quitToSceneID = "";

    private void Start()
    {
        // Hide Menu
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);

        // Set quit to main scene if empty
        if (quitToSceneID == "")
            quitToSceneID = "Main Menu";
    }

    public void TogglePause()
    {
        if (!gameIsPaused)
        {
            Pause();
        }

        else
        {
            Resume();
        }

        //Debug.Log("Game Paused: " + gameIsPaused.ToString());
    }

    // Pause
    public void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    // Resume

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }


    // Change to Main Menu

    public void Quit()
    {
        SceneManager.LoadScene(quitToSceneID);
    }
}
