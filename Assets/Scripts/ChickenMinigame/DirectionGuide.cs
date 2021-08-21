using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ChickenGameplay.Navigate
{
    [SelectionBase]

    public class DirectionGuide : MonoBehaviour
    {

        [SerializeField] Vector2[] directionPointer = new Vector2[4];// Only Normalized Vectors

        // always Flip Between Verical and Horizontal
        [SerializeField] bool alwaysFlipDirection = false;


        // Unity Functions
        private void Start()
        {
            // Sprite Editor Only
            SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();


            if (sprite) sprite.enabled = false;
        }


        // Custom Functions
        public Vector2 GetSetDirection(Vector2 currentDirection)
        {
            int newIndex = Random.Range(0, 127) % directionPointer.Length;

            // Go to Next if it is exception
            if (alwaysFlipDirection)
            {
                while (currentDirection == directionPointer[newIndex] || currentDirection == -directionPointer[newIndex])
                {
                    newIndex = (newIndex + 1) % directionPointer.Length;
                }
            }

            return directionPointer[newIndex];
        }


        private void OnDrawGizmos()
        {
            float pivotSize = .075f;

            Gizmos.DrawWireCube((transform.position), Vector3.one * pivotSize);

            foreach (Vector2 dir in directionPointer)
            {
                Gizmos.color = Color.green;

                float lenght = .4f;
                float tipSize = .04f;

                // Main
                Gizmos.DrawRay(transform.position, dir * lenght);

                //Tip
                Gizmos.DrawWireCube((transform.position + (Vector3) dir * lenght), Vector3.one * tipSize);
            }
        }
    }
}
