using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Status
{
    [System.Serializable]
    [CreateAssetMenu(fileName = " New AI Data", menuName = "Figther AI Data")]
    public class AIFighterData : ScriptableObject
    {
        // Perlin Noise Variables
        public float perlinScale = 8;        // Distance in Spikes

        // Delay Variables
        public float thinkDelayMin = .1f;
        public float thinkDelayMax = .6f;

        // Distance Variation To Attack
        public float attackThreshold = 0.2f;
    }
}
