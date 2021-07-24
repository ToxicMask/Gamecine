using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Chicken;

namespace ChickenGameplay.Navigate
{
    public class DeadLine : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<RunnerChicken>())
            {
                print("CABOU O JOGO!");
                Time.timeScale = 0;
            }
        }

    }
}
