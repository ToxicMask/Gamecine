using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControl : MonoBehaviour
{
    [Range(0, 8)]
    public float cursorSpeed = 4f;

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 move = (moveX * Vector2.right) + (moveY * Vector2.up);

        transform.position = (Vector2)transform.position + ((move * Time.deltaTime) * cursorSpeed);
    }
}
