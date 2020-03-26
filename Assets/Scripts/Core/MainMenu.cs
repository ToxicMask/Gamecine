using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Core.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void StartMinigame(int sceneNumber)
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}