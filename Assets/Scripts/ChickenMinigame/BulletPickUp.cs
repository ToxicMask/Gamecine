using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Score;
using ChickenGameplay.Chicken;

namespace ChickenGameplay.PickUp
{
    public class BulletPickUp : MonoBehaviour
    {

        public AudioClip shootSFX = null; 

        public float stunTime = 2.1f;
        public int pointsValue = 50;


        public void PickUp()
        {
            SoundController.Instance.SetSfx(shootSFX);
            ScoreManager.instance.AddScore(pointsValue);
            RunnerChicken.current.SetStunTime(stunTime);
            Destroy(gameObject);
        }

    }
}
