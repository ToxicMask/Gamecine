using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set up Tempo to maintain constant Rythum
/// </summary>

namespace RythumProto.BeatTrack
{
    public class BPMTempo : MonoBehaviour
    {
        public static BPMTempo currentTempo = null;

        float beatTimer = 0f;
        int beatCount = 0;

        public bool active = true;

        [SerializeField]  float beatsPeerMinute = 60;

        // Spawner Track Componnent
        MainTrack mainTrack;


        #region Singleton
        // Set Current as only instance
        private void Awake()
        {
            currentTempo = this;
        }

        // Destoy same instance
        private void OnDestroy()
        {
            if (currentTempo == this) currentTempo = null;
        }
        #endregion



        private void Start()
        {
            //Auto Get
            if (!mainTrack) mainTrack = GetComponent<MainTrack>();
        }

        void Update()
        {
            if (PauseSystem.gameIsPaused || !active) return;

            beatTimer += Time.deltaTime;

            bool beatFull = BeatDetection(ref beatTimer);

            // Fufill Next Beat
            if (beatFull) mainTrack.NextBeat();
        }



        bool BeatDetection(ref float time)
        {
            bool beatFull = false;
            float beatInterval = 60f / beatsPeerMinute;

            // Detect Beat and Restore Timer
            if (beatTimer >= beatInterval)
            {
                beatFull = true;
                beatTimer -= beatInterval;
                beatCount++;
            }

            return beatFull;
        }
    }
}
