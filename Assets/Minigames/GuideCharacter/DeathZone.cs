using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.GetComponent<AutomaticCharacter>())
            {
                Destroy(collider.gameObject);
            }
        }
    }
}
