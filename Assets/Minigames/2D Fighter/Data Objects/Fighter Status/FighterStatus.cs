using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Status
{
    [System.Serializable]
    [CreateAssetMenu(fileName = " New Figther Status", menuName = "Figther Status Object")]
    public class FighterStatus : ScriptableObject
    {
        // Attack
        public float timeBetweenAttacks = 0.8f;
        public float attackRange = 0.65f;
        public float attackStrongDamage = 3f;
        public float attackWeakDamage = 1f;

        // Health
        public float maxHealth = 15f;

        // Movement
        public float walkSpeed = 1.2f;
        public float knockbackSpeed = 1.8f;

    }
}
