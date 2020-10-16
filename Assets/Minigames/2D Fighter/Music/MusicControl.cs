using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Music
{
    public class MusicControl : MonoBehaviour
    {
        [SerializeField]  AudioSource levelMusic = null;
        [SerializeField] AudioEchoFilter musicEchoFilter = null;
        [SerializeField] AudioLowPassFilter musicLowFilter = null;
        [SerializeField] AudioDistortionFilter musicDistortion = null;

        // Start is called before the first frame update
        void Start()
        {
            if (levelMusic == null) levelMusic = GetComponent<AudioSource>();
            if (musicEchoFilter == null)  musicEchoFilter = GetComponent<AudioEchoFilter>();
            if (musicLowFilter == null) musicLowFilter = GetComponent<AudioLowPassFilter>();
            if (musicDistortion == null) musicDistortion = GetComponent<AudioDistortionFilter>();
        }

        private void Update()
        {
            //Pause if is pause
            if (levelMusic.isPlaying && PauseSystem.gameIsPaused) levelMusic.Pause();

            // Resume if paused
            if (!levelMusic.isPlaying && !PauseSystem.gameIsPaused) levelMusic.UnPause();
        }


        public void PlayStandardMusic()
        {
            levelMusic.Play();
            levelMusic.pitch = 1f;

            musicLowFilter.enabled = false;
            musicEchoFilter.enabled = false;
            musicDistortion.enabled = false;
        }

        public void IntroMusic()
        {
            levelMusic.Play();
            levelMusic.pitch = .5f;

            musicDistortion.enabled = false;

            musicLowFilter.enabled = true;
            musicLowFilter.cutoffFrequency = 6000;

            musicEchoFilter.enabled = true;
        }

        public void PlayEndMusic()
        {
            levelMusic.Play();
            levelMusic.time = 0f;
            levelMusic.pitch = .4f;

            musicLowFilter.enabled = true;
            musicLowFilter.cutoffFrequency = 1000;
            musicEchoFilter.enabled = false;
        }

        public void Stop()
        {
            levelMusic.Stop();
        }
    }
}
