using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;
    public static SoundController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<SoundController>();
            }
            return instance;
        }
    }

    public AudioSource SfxSource, OstSource;
    public AudioClip ostClip;
    public void SetSfx(AudioClip clip) {
        if (clip == null) {
            Debug.Log("NO CLIP ADDED");
            return;
        }
        SfxSource.PlayOneShot(clip);
    }
    public void SetOst(AudioClip clip) {
        if (clip == null) {
            Debug.Log("NO CLIP ADDED");
            return;
        }
        OstSource.clip = clip;
        OstSource.Play();
    }
    private void Awake() {
        SetOst(ostClip);
    }
    public void PlayOst() {
        OstSource.Play();
    }
    public void StopOst() {
        OstSource.Stop();
    }
}
