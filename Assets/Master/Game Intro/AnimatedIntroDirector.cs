using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

namespace Cinematic.Intro
{
    public class AnimatedIntroDirector : MonoBehaviour
    {

        public string nextScene = "Main Menu1";

        public Image imageFadeLayer;

        public VideoPlayer introVideo;

        // Start is called before the first frame update
        void Start()
        {
            ShowVideo();

            // Fade In
            FadeIn();

            //Video
            Invoke("PlayVideo", .2f);

            // Fade Out
            Invoke("FadeOut", 3.75f);
        }

        void ShowVideo()
        {
            introVideo.Play();
            introVideo.Pause();
        }

        void FadeIn()
        {
            LeanTween.alpha(imageFadeLayer.rectTransform, 0f, 1.0f).setEaseInCubic();
        }

        void PlayVideo()
        {
            introVideo.Play();
        }

        void FadeOut()
        {
            imageFadeLayer.color = new Color(1, 1, 1, 0); // Transparent White

            LeanTween.alpha(imageFadeLayer.rectTransform, 1f, 1.5f).setEaseInCubic().setOnComplete(GoToMainMenu) ;
        }

        void GoToMainMenu()
        {
            SceneManager.LoadScene("Main Menu1");
        }
    }
}
