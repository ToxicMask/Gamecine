using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ChickenPrototype.Navigate
{


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
            foreach (Vector2 dir in directionPointer)
            {
                Gizmos.color = Color.green;

                Gizmos.DrawRay(transform.position, dir);
            }
        }
    }
}
