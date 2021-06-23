using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Prototypes.Frogger
{
    public class EndLine : MonoBehaviour
    {
        Collider2D area;

        void Awake()
        {
            area = GetComponent<Collider2D>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                FroggerGameManager.current.LevelCompleted();
            }

        }
    }
}
