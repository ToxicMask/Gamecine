using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Timer
{
    
    public float elapsed;
    public float setTime;

    public event Action OnComplete = delegate { };

    public bool complete {
        get { return elapsed >= setTime; }
    }

    public Timer(float _set) {
        setTime = _set;
    }

    public void Update() {
        elapsed += Time.deltaTime;

        if (elapsed >= setTime) {
            OnComplete();
        }
        
    }

    public void Reset() {
        elapsed = 0;
    }
}
