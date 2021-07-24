using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.GameManager;

namespace ChickenGameplay.PickUp
{
    public class EggPickUp : MonoBehaviour
    {
        public int pointsValue = 100;


        public void PickUp()
        {
            ChickenLevelManager.instance.playerScore += pointsValue;

            Destroy(gameObject);
        }
    }
}
