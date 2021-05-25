using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Basic Structure To Store Information about a Beat in Beat Track
/// </summary>

namespace RythumProto.BeatTrack
{
    [System.Serializable]
    public struct NoteBinary
    {
        public bool left;
        public bool down;
        public bool up;
        public bool right;

        public bool trickA;
        public bool trickB;
    }
}
