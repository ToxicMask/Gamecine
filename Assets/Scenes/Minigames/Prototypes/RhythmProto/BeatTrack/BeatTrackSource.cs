using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set Info To Scriptable object - New Tracks
/// </summary>

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
