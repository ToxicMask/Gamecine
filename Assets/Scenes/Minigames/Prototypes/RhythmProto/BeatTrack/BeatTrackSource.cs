using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythumProto.BeatTrack
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "newBeatTrack", menuName = "BeatTrack/New Beat Array")]
    public class BeatTrackSource : ScriptableObject
    {
        // Input
        public NoteBinary[] beatTrackSource;
    }
}
