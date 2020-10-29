using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;

    public float delay = 1f;

    public float repeat = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (prefab)
        {
            InvokeRepeating("Spawn", delay, repeat);
        }
    }

    protected virtual void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
