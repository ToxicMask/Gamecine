using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Prototypes.Lemmings
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Paulista>()) Destroy(collision.gameObject);
        }
    }
}
