using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Score;

namespace ChickenGameplay.PickUp
{
    public class EggPickUp : MonoBehaviour
    {
        [SerializeField] int pointsValue = 100;


        public void PickUp()
        {
            ScoreManager.instance.AddScore(pointsValue);

            Destroy(gameObject);
        }
    }
}
