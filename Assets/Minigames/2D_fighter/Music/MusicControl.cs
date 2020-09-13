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

            musicDistortion.enabled = true;

            musicLowFilter.enabled = true;
            musicLowFilter.cutoffFrequency = 6000;

            musicEchoFilter.enabled = false;
        }

        public void PlayEndMusic()
        {
            levelMusic.Play();
            levelMusic.pitch = .2f;

            musicLowFilter.enabled = true;
            musicLowFilter.cutoffFrequency = 800;
            musicEchoFilter.enabled = true;
        }

        public void Stop()
        {
            levelMusic.Stop();
        }
    }
}
