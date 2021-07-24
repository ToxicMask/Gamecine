using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGameplay.Chick
{
    public class Egg : MonoBehaviour
    {

        public float timerValue = .5f;
        float timerLeft = 10f;


        private void Start()
        {
            timerValue = timerLeft;
        }

        private void Update()
        {
            // Update Timer
            timerLeft -= Time.deltaTime;

            if (timerLeft < 0)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {


            if (collision.collider.tag == "Player")
            {
                print(collision.collider + "@");
                Destroy(gameObject);
            }
        }
    }
}
