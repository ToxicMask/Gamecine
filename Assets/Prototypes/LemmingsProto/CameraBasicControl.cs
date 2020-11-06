using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.Lemmings
{
    public class CameraBasicControl : MonoBehaviour
    {

        public float cameraSpeed = 16f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            Vector3 move = (moveX * Vector3.right) + (moveY * Vector3.up);

            transform.position = transform.position + (move * Time.unscaledDeltaTime);
        }
    }
}