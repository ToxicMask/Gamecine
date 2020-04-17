﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{

    // Public Static var --> Acessible to get; private to set
    public static bool gameIsPaused; // { get { return gameIsPaused; } private set { gameIsPaused = value; } }

    // Pause UI
    public GameObject pauseButtom;
    public GameObject pauseMenuUI;

    // Main Menu ID
    [SerializeField] string quitToSceneID = "";

    private void Start()
    {
        // Show Buttom
        if (pauseButtom != null) pauseButtom.SetActive(true);

        // Hide Menu
        if (pauseMenuUI != null) pauseMenuUI.SetActive(false);

        // Set quit to main scene if empty
        if (quitToSceneID == "") quitToSceneID = "Main Menu";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) TogglePause(); // ESC --> Pause Game
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

        // Buttom
        if (pauseButtom != null)
            pauseButtom.SetActive(false);

        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    // Resume

    public void Resume()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;

        // Buttom
        if (pauseButtom != null)
            pauseButtom.SetActive(true);

        // Menu
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }


    // Change to Main Menu

    public void Quit()
    {
        SceneManager.LoadScene(quitToSceneID);
    }
}
