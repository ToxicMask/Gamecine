using Sidescroller.Fighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{

    [SerializeField] float maxHealth = 10;
    public float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update(){}

    public void TakeDamage(float damage)
    {
        if (currentHealth - damage <= 0) { Die(); }
        else
        {
            currentHealth -= damage;
            print(this.name + " took damage");
            print(currentHealth);
        }
    }
    void Die()
    {
        print(this.name + " died");
        //this.GetComponent<SideScrollerFighter>().ChangeState(FighterState.Dead);
        this.GetComponent<SideScrollerFighter>().Death();
        this.GetComponent<Collider2D>().enabled = false;
    }
}
