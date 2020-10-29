using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformSpawner : Spawner
{

    public float plataformSpeed = 8;

    public Vector3 plataformDirection = Vector3.right;

    protected override void Spawn()
    {
        GameObject plataform = prefab;

        MoverPlataform mover = plataform.GetComponent<MoverPlataform>();

        if ( mover)
        {
            mover.SetPlataformConfig( plataformSpeed, plataformDirection);
        }

        print("Here");
        Instantiate(plataform, transform.position, transform.rotation);
    }
}
