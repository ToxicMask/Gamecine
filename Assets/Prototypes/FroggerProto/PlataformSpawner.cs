using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSpawner : Spawner
{

    public float plataformSpeed = 8;
    public Vector3 plataformDirection = Vector3.right;

    public bool selfDescruct = false;
    public float selfDestructDelay = 20f;


    protected override void Spawn()
    {
        GameObject plataform = prefab;

        MoverPlataform mover = plataform.GetComponent<MoverPlataform>();
        DestroySelfScript destruct = plataform.GetComponent<DestroySelfScript>();


        if ( mover)
        {
            mover.SetPlataformConfig( plataformSpeed, plataformDirection);
        }

        if (destruct && selfDescruct)
        {
            destruct.delay = this.selfDestructDelay;
        }


        //print("Here");
        Instantiate(plataform, transform.position, transform.rotation);
    }
}

