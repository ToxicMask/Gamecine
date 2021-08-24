using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    #region Static Variables

    public static GameManager current;

    #endregion
    

    [SerializeField] GameObject PauseCanvas = null;

    public AudioControl audioControl = null;


    #region Unity Methods

    protected virtual void Awake()
    {
        // Set single instance
        current = this;

        if (!PauseCanvas) Debug.LogAssertion("No Canvas Selected !!!");
    }

    protected virtual void Start()
    {
        /////////////////////////
        /// Config Game Scene ///
        /////////////////////////
    }

    // Check Current Minigamestate
    protected virtual void Update()
    {
        /// Check Methods realise Methods acording to the current State///
        ProcessCurrentMinigameState();
    }

    #endregion

    #region Process Methods

    protected virtual void ProcessCurrentMinigameState()
    {

    }

    #endregion

    #region Check Methods

    protected virtual void CheckVictory()
    {

    }

    protected virtual void CheckDefeat()
    {

    }

    #endregion

    #region Change Scenes

    public void ChangeToMainMenuScene()
    {
        SceneManager.LoadScene((int)AllScenes.Main_Menu);
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
