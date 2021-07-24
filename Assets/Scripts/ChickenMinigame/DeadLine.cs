using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenPrototype.Chicken;

namespace ChickenPrototype.Navigate
{
    public class DeadLine : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<Chick>())
            {
                print("CABOU O JOGO!");
                Time.timeScale = 0;
            }
        }

    }
}
