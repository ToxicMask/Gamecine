using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RythumProto.Camera
{
    public class DrawCameraGizmo : MonoBehaviour
    {

        public Vector2 cameraSize = Vector2.zero;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(transform.position, cameraSize);
     
        }
    }
}
