using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LemmingsGameManager : MonoBehaviour
{
    public int paulistasCurrent = 0;
    public int paulistasDone = 5;

    public LemmingsExit paulistaExit = null;

    public void Awake()
    {
        paulistasCurrent = 0;
    }

    private void LateUpdate()
    {
        if (paulistaExit)
        {
            paulistasCurrent = paulistaExit.exitedCount;
        }

        CheckVictory();
    }


    void CheckVictory()
    {
        if (paulistasCurrent >= paulistasDone)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        print("Victory");
        Invoke("ResetGame", 3f);

        return;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
