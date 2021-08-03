﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Score;
using ChickenGameplay.Chicken;

namespace ChickenGameplay.PickUp
{
    public class BulletPickUp : MonoBehaviour
    {
        
        public float stunTime = 2.1f;
        public int pointsValue = 50;


        public void PickUp()
        {
            ScoreManager.instance.AddScore(pointsValue);
            RunnerChicken.current.SetStunTime(stunTime);
            Destroy(gameObject);
        }

    }
}
