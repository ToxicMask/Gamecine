using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RythumProto.Camera
{
    public class DrawCameraGizmo : MonoBehaviour
    {
        public bool active = false;

        public Vector2 cameraSize = Vector2.zero;

        private void OnDrawGizmos()
        {
            if (!active) return;

            Gizmos.color = Color.green;

            Gizmos.DrawWireCube(transform.position, cameraSize);
     
        }
    }
}
