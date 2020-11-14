using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCommandControl : MonoBehaviour
{

    // Expression
    private bool mainClick => Input.GetButtonDown("Action Primary");

    private float cursorRadius = .04f;
    
    void LateUpdate()
    {
        if (mainClick)
        {
            Collider2D[] clickedObjects = Physics2D.OverlapCircleAll(transform.position, cursorRadius);

            foreach ( Collider2D clicked in clickedObjects)
            {
                ICursorInteractable interact = clicked.GetComponent<ICursorInteractable>();
                if (interact != null)
                {
                    // Only Once
                    interact.Interact();
                    return;
                }
            }
        }
    }
}
