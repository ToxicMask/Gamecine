using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfScript : MonoBehaviour
{

    public float delay = 12f;

    
    void Start()
    {
        Invoke("DestroySelf", delay);
    }

    public void DestroySelf()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
