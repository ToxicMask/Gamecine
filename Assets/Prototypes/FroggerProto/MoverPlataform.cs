using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverPlataform : Plataform
{

    public float speedH = 8;
    public Vector3 moveDirection = Vector3.right;

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speedH * Time.fixedDeltaTime;
    }

    public void SetPlataformConfig(float speed, Vector3 direction)
    {
        this.speedH = speed;
        this.moveDirection = direction;
    }
}
