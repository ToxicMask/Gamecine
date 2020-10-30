using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FroggerGameManager : MonoBehaviour
{

    public static FroggerGameManager current;

    public static bool gameEnd = false;

    public Frogger playerFrogger = null;

    

    void Awake()
    {
        current = this;

        gameEnd = false;
    }

    // Check Conditions
    void LateUpdate()
    {
        if (gameEnd) return;


        // Check
        CheckDefeat();

    }


    void CheckDefeat()
    {
        // Check Defeat
        if (!playerFrogger)
        {
            gameEnd = true;
            print("Defeat");
            Invoke("ResetGame", 3f);
            return;
        }
    }

    public void LevelCompleted()
    {
        gameEnd = true;
        print("Victory");
        Invoke("ResetGame", 3f);

        return;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
