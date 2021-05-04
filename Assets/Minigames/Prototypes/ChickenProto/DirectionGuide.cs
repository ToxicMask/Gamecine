using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGuide : MonoBehaviour
{
    public static readonly Vector2[] allDirections = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

    [SerializeField] Vector2[] directionPointer = new Vector2[4];// Only Normalized Vectors

    // always Flip Between Verical and Horizontal
    [SerializeField] bool alwaysFlipDirection = false;

    private void Start()
    {
        // Sprite Editor Only
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();


        if (sprite) sprite.enabled = false;
    }

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


    Vector2 GetRandomDirection(Vector2 excepition)
    {
        
        int newIndex = Random.Range(1, 1000000) % 4;

        // Go to Next if it is exception
        if (excepition == allDirections[newIndex]) newIndex = (newIndex + 1) % 4;

        return allDirections[newIndex];
    }

}
