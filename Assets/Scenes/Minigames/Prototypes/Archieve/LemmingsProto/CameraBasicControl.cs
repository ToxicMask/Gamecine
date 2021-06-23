using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.Lemmings
{
    public class CameraBasicControl : MonoBehaviour
    {

        public float cameraSpeed = 16f;

        // Update is called once per frame
        void LateUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = (moveX * Vector3.right) + (moveY * Vector3.up);

            // Update X/Y Positions
            transform.position = transform.position + (move * Time.unscaledDeltaTime);
        }
    }
}