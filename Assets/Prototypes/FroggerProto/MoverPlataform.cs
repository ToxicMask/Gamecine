using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataform : MonoBehaviour
{
    [SerializeField] Collider2D collision;
    [SerializeField] Rigidbody2D rb;

    public float speedH = 8;
    private Vector3 direction = Vector3.right;

    void Awake()
    {
        //Auto Get
        rb = GetComponent<Rigidbody2D>();


    }

    private void FixedUpdate()
    {
        rb.velocity = direction * speedH * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent == gameObject.transform && collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }

    public void SetPlataformConfig(float speed, Vector3 direction)
    {
        this.speedH = speed;
        this.direction = direction;
    }
}
