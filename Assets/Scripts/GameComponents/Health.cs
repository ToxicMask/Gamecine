using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    public int maxHealth;
    public int Current{
        get{return health;}
        set{
            health = value;
            if(IsDead()){
                Destroy(this.gameObject);
            }
        }
    }
    public bool IsDead(){
        if(health <= 0) return true;
        return false;
    }
    private void Start() {
        health = maxHealth;
    }
}
