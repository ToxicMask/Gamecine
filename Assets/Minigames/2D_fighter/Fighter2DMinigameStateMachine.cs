using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sidescroller.Fighting;
using Sidescroller.Control;

public enum MinigameState
{
    Intro,
    IntroTransiction,
    Gameplay,
    EndTransiction,
    End
}


public class Fighter2DMinigameStateMachine : MonoBehaviour
{

    MinigameState currentState;

    [SerializeField] SideScrollerFighter fighter1 = null;
    [SerializeField] SideScrollerFighter fighter2 = null;

    [SerializeField] Camera introCamera = null;
    [SerializeField] Camera gameCamera = null;

    // Awake is called before the first frame update
    void Start()
    {
        // Config Game Scene

        if (!CheckPublicVariables()) return;

        currentState = MinigameState.Intro;

        // Deactivate Player Controllers
        ConfigFighterControllers( false );

        // Config Cameras
        ConfigSceneCamera();
    }

    // Check Current Minigamestate
    void Update()
    {
        CheckCurrentMinigameState();
    }

    bool CheckPublicVariables()
    {
        // End Function
        if (fighter1 == null || fighter2 == null)
        {
            Debug.LogAssertion("No Fighters Selected !!!");
            return false;
        }

        if (introCamera == null || gameCamera == null)
        {
            Debug.LogAssertion("No Cameras Selected !!!");
            return false;
        }

        //Dont end Function
        return true;
    }

    void ConfigFighterControllers( bool controllersEnabled )
    {
        // Get Controller Components

        SideScrollerController controller1 = fighter1.GetComponent<SideScrollerController>();
        SideScrollerController controller2 = fighter2.GetComponent<SideScrollerController>();


        if (controller1 == null || controller2 == null)
        {
            Debug.LogAssertion(" No Controlers Found ");
            return;
        }

        // Deactivate Players for Intro Sequence (bool)

        controller1.enabled = controllersEnabled;
        controller2.enabled = controllersEnabled;

        // Set Fighters to Stand Still
        controller1.SetCharacterToStandStill();
        controller2.SetCharacterToStandStill();
    }

    // Change Camera acording to Minigame State
    void ConfigSceneCamera()
    {
        switch ( currentState )
        {
            case (MinigameState.Intro):
                introCamera.enabled = true;
                gameCamera.enabled = false;
                break;

            case (MinigameState.Gameplay):
                introCamera.enabled = false;
                gameCamera.enabled = true;
                break;
        }
    }

    public void ChangeCurrentMinigameState(MinigameState newState)
    {
        currentState = newState;
    }

    void CheckCurrentMinigameState()
    {
        // Check Current game state
        switch (currentState)
        {

            case (MinigameState.Intro):
                if (Input.GetButtonDown("Submit"))
                {
                    //Debug.Log("GAME START");

                    // Activate Player Controllers
                    ConfigFighterControllers(true);

                    // Change State
                    currentState = MinigameState.Gameplay;

                    ConfigSceneCamera();
                }

                break;

            case (MinigameState.Gameplay):

                CheckForDefeatedFighter();
                break;

            case (MinigameState.End):
                if (Input.GetButtonDown("Submit"))
                {
                    Debug.Log("GAME END");
                }

                break;
        }
    }

    void CheckForDefeatedFighter()
    {
        if (fighter1.currentState == FighterState.Dying)
        {
            //Debug.LogAssertion("Victory PLAYER 2");
            currentState = MinigameState.End;
            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            Invoke("ResetLevel", 2.5f);  // TEMP
        }

        if (fighter2.currentState == FighterState.Dying)
        {
            //Debug.LogAssertion("Victory PLAYER 1");
            currentState = MinigameState.End;
            // Deactivate Player Controllers
            ConfigFighterControllers(false);

            Invoke("ToMainMenu", 2.5f); // TEMP
        }
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    void ResetLevel()
    {
        SceneManager.LoadScene("2D_Fighter");
    }
}
