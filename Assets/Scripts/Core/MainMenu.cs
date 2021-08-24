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
        public static void LoadByName(string name){
            var str = "";
            char bannedChar = '_';
            for(int i = 0; i < name.Length; i++){
                if(name[i] == bannedChar){
                    continue;
                }
                str += name[i];
            }
            SceneManager.LoadScene(str);
        }
    }
}