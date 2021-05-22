using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Timer;

public class SpawnNotes : MonoBehaviour
{

    // Prefab
    public GameObject notePrefab;

    public void Spawn()
    {
        //Instanciate Node
        GameObject.Instantiate(notePrefab, gameObject.transform);
    }
}
