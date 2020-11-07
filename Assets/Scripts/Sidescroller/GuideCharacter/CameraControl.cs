using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    [RequireComponent(typeof(Camera))]
    public class CameraControl : MonoBehaviour
    {
        // Variables
        public Transform playerCursor = null;

        public float dragRadius = 3f;
        [Range(0.01f, 0.5f)]
        public float dragPrecisonStep = 0.01f;

        // Expressions
        private Vector2 OffsetToCursor => (Vector2)playerCursor.position - (Vector2)this.transform.position;
        private Vector2 DirectionToCursor => OffsetToCursor.normalized;
        private float DistanceToCursor => OffsetToCursor.magnitude;

        // Follow Player Cursor
        void LateUpdate()
        {

            if (!playerCursor) return;

            while (dragRadius < DistanceToCursor)
            {
                
                Vector2 newPosition =   (DirectionToCursor * dragRadius) * dragPrecisonStep;
                transform.position += (Vector3)newPosition;

                // Security Measure
                Debug.Assert(DistanceToCursor < 10f);
            }

        }
    }
}
