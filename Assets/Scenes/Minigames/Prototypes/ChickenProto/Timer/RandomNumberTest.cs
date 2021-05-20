using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenPrototype.Timer;

public class RandomNumberTest : TimerTarget
{

    public int times = 1000;


    override public void TimeOut()
    {
        PrintMediumI(times); // INT
        PrintMediumF(times); // Float
    }


    void PrintMediumI(int times)
    {
        int medium = 0;
        int min = 0, max= 10;

        for (int i = 0; i< times; i++)
        {
            medium += Random.Range(min, max);
        }

        medium = medium / times;

        print("Random Int: " + medium.ToString());
    }

    void PrintMediumF(int times)
    {
        float medium = 0;

        float min = 0f, max = 10f;

        for (int i = 0; i < times; i++)
        {
            medium += Random.Range(min, max);
        }

        medium = medium / times;

        print("Random Float: " + medium.ToString());
    }
}
