using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A code to close the aplication in the scene


public class ExitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
