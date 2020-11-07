using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Requires Audio Source
[RequireComponent(typeof(AudioSource))]

public class AudioControl : MonoBehaviour
{
    #region Static Variables

    public static AudioControl current;

    #endregion

    // Level Music
    [SerializeField] AudioSource levelMusic = null;

    protected virtual void Start()
    {
        if (levelMusic == null) levelMusic = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        //Pause if is pause
        if (levelMusic.isPlaying && PauseSystem.gameIsPaused) levelMusic.Pause();

        // Resume if paused
        if (!levelMusic.isPlaying && !PauseSystem.gameIsPaused) levelMusic.UnPause();
    }

    public void PlayStandardMusic()
    {
        levelMusic.Play();
    }

    public void Stop()
    {
        levelMusic.Stop();
    }
}
