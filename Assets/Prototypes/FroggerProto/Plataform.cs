using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    void Awake()
    {
        //Auto Get
        rb = GetComponent<Rigidbody2D>();

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
}
