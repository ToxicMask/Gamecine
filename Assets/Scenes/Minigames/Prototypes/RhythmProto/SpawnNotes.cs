using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Timer;

public class SpawnNotes : TimerTarget
{

    // Prefab
    public GameObject notePrefab;

    public override void TimeOut()
    {
        base.TimeOut();
        print("!");
        //Instanciate Node
        GameObject.Instantiate(notePrefab, gameObject.transform);
    }

}
