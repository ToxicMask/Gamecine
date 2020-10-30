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

    protected virtual void Spawn(Vector3 newPosition)
    {
        Instantiate(prefab, newPosition, transform.rotation);
    }

    protected virtual void Spawn(GameObject modPrefab)
    {

        Instantiate(modPrefab, transform.position, transform.rotation);
    }

    protected virtual void Spawn(GameObject modPrefab, Vector3 newPosition)
    {
        
        Instantiate(modPrefab, newPosition, transform.rotation);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
