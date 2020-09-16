using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.ChangeScene
{
    public class MainMenu : MonoBehaviour
    {
        public static void StartMinigame(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}