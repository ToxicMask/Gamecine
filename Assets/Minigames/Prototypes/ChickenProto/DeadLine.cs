using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("CABOU O JOGO!");
        Time.timeScale = 0;
    }
}
