using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cinematic.Intro
{
    public class StartScript : MonoBehaviour
    {

        public void StartIntro()
        {
            SceneManager.LoadScene((int)AllScenes.Animated_Intro);
        }
    }
}

