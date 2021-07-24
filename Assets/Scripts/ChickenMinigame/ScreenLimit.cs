using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Player;

namespace ChickenGameplay.Camera
{
    public class ScreenLimit : MonoBehaviour
    {

        public CameraFollowPlayer followScript;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<SeekerPlayer>())
            {
                print("Change!");
                if (followScript) followScript.MoveTo(transform.position);
            }
        }
    }
}
