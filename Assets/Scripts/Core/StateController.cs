using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    private static StateController instance;
    public static StateController Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<StateController>();
            }
            return instance;
        }
    }
    public States currentState;
    public void ChangeState(States state){
        if(currentState == state){
            return;
        }
        currentState = state;
    }
}
public enum States{
    GAME_UPDATE,
    GAME_WAIT
}
