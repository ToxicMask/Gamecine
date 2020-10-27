using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Audio
{
    public class SideScrollerAudio : MonoBehaviour
    {

        // Components
        public AudioListener audioListener;

        // Audio Clips
        [SerializeField] AudioClip audioAttackGrunt = null;
        [SerializeField] AudioClip audioAttackHit = null;
        [SerializeField] AudioClip audioAttackBlock = null;
        [SerializeField] AudioClip audioDamaged = null;
        [SerializeField] AudioClip audioDeath = null;

        //
        public void PlayClip(AudioClips id, float volume = 1f)
        {
            // Audio Clip
            AudioClip audioClip = null;

            switch (id)
            {
                case AudioClips.AttackGrunt:
                    audioClip = audioAttackGrunt;
                    break;
                case AudioClips.AttackHit:
                    audioClip = audioAttackHit;
                    break;
                case AudioClips.AttackBlocked:
                    audioClip = audioAttackBlock;
                    break;
                case AudioClips.Damaged:
                    audioClip = audioDamaged;
                    break;
                case AudioClips.Death:
                    audioClip = audioDeath;
                    break;
            }

            // Play Audio at Listener Location
            if (audioClip) AudioSource.PlayClipAtPoint(audioClip, audioListener.transform.position, volume);
        }
    }

    public enum AudioClips
    {
        
        AttackGrunt,
        AttackHit,
        AttackBlocked,
        Damaged,
        Death,
    }
}
