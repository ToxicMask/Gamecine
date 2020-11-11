using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{ 
    public class DynamicWorldBoundry : MonoBehaviour
    {
        // Components
        Collider2D cursorArea = null;

        // Expressions
        float BoundryX => (cursorArea.bounds.size.x / 2f) + transform.position.x;
        float BoundryY => (cursorArea.bounds.size.y / 2f) + transform.position.y;

        void Awake()
        {
            // Auto Get Components
            cursorArea = GetComponent<Collider2D>();

            // Config
            cursorArea.offset = Vector2.zero;

        }

        public Vector2 ClampPosition(Vector2 currentPosition)
        {

            float x = Mathf.Clamp(currentPosition.x, -BoundryX, BoundryX);
            float y = Mathf.Clamp(currentPosition.y, -BoundryY, BoundryY);

            return new Vector2(x, y);
        }

    }
}
