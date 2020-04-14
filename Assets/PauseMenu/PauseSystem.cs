using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{

    // Public Static var
    public static bool gameIsPaused = false;

    // Pause Menu
    public GameObject pauseMenuUI;

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
}
