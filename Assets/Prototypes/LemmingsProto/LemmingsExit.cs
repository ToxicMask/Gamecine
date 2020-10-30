using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingsExit : MonoBehaviour
{
    public int exitedCount = 0;

    private void Awake()
    {
        exitedCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Paulista>())
        {
            exitedCount += 1;
            Destroy(collision.gameObject);
        }
    }
}
