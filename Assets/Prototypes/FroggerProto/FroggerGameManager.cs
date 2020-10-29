using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FroggerGameManager : MonoBehaviour
{

    public static FroggerGameManager current;

    public Frogger playerFrogger = null;

    void Awake()
    {
        current = this;  
    }

    // Check Conditions
    void LateUpdate()
    {
        // Check
        CheckDefeat();

    }


    void CheckDefeat()
    {
        // Check Defeat
        if (!playerFrogger)
        {
            print("Defeat");
            Destroy(gameObject);
            return;
        }
    }

    public void LevelCompleted()
    {

        print("Victory");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        return;
    }
}
