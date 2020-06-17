using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sidescroller.Fighting;

public class Fighter2DResult : MonoBehaviour
{

    [SerializeField] SideScrollerFighter Fighter1 = null;
    [SerializeField] SideScrollerFighter Fighter2 = null;


    // Start is called before the first frame update
    void Start()
    {
        // Self Destroy
        if (Fighter1 == null || Fighter2 == null)
        {
            Debug.LogAssertion("No Fighters Selected === Auto Destruct");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Fighter1.currentState == FighterState.Dead)
        {
            //Debug.LogAssertion("Victory PLAYER 2");

            Invoke("ResetLevel", 2.5f);
        }

        if (Fighter2.currentState == FighterState.Dead)
        {
            //Debug.LogAssertion("Victory PLAYER 1");

            Invoke("ToMainMenu", 2.5f);
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
