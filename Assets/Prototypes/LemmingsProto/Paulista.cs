using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PaulistaType
{
    Pedestre,
    Guarda
}


public class Paulista : MonoBehaviour
{

    public PaulistaType currentType = PaulistaType.Pedestre;

    [Range(-1, 1)]
    [SerializeField] int walkDirection = 1;
    [SerializeField] float walkSpeed = 8f;

    // Ground Check
    private float distanceToGround = .1f;
    private float rangeGroundCheck = .1f;
    public float fallDiff = .01f;

    // Gravity
    public float fallVelocity = 1f;

    Rigidbody2D rb = null;
    



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Collider2D collider = GetComponent<Collider2D>();

        if (collider)
        {
            distanceToGround = collider.bounds.extents.y;
            rangeGroundCheck = collider.bounds.extents.x;
        }
    }

    private void OnMouseOver()
    {
        if (!Input.GetButtonDown("Action Primary")) return;
        if (!CheckIsGrounded()) return;

        switch (currentType)
        {
            case PaulistaType.Pedestre:
                GetComponent<SpriteRenderer>().color = Color.red;
                currentType = PaulistaType.Guarda;
                break;

            case PaulistaType.Guarda:
                GetComponent<SpriteRenderer>().color = Color.green;
                currentType = PaulistaType.Pedestre;
                break;
        }
    }

    void FixedUpdate()
    {
        switch (currentType)
        {
            case PaulistaType.Pedestre:
                Move();
                break;

            case PaulistaType.Guarda:
                Stop();
                break;
        }
    }

    private void Move()
    {
        if (CheckIsGrounded())
        {

            //print("H");
            // Walk
            Vector2 linearVelocity = walkDirection * walkSpeed * Vector2.right;

            rb.velocity = (Time.fixedDeltaTime * linearVelocity);
        }

        else
        {
            //print("V");
            // Only Verical
            rb.velocity = fallVelocity * Vector3.down;
        }
    }

    private void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    // When other paulistas enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Paulista paulista = collision.GetComponent<Paulista>();

        if (paulista)
        {
            switch (currentType)

              {
            case PaulistaType.Pedestre:
                break;

            case PaulistaType.Guarda:
                    if (paulista.currentType == PaulistaType.Pedestre)
                    {
                        paulista.walkDirection *= -1;
                    }
                    break;
            }


        }
    }

    bool CheckIsGrounded()
    {
        bool rightTrigger = Physics2D.Raycast(transform.position + ( rangeGroundCheck * Vector3.right), Vector3.down, distanceToGround + fallDiff);
        bool leftTrigger = Physics2D.Raycast(transform.position + (rangeGroundCheck * Vector3.left), Vector3.down, distanceToGround + fallDiff);

        return leftTrigger || rightTrigger;
    }
}
