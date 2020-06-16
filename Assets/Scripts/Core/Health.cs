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
        // Death Transition
        if (currentHealth - damage <= 0) { Die(); }

        // Damage Transition (If not Damaged or Dead)
        else 
        {

            SideScrollerFighter scrollerFighter = this.GetComponent<SideScrollerFighter>();

            FighterState state = scrollerFighter.currentState;

            // Do Damage if is dead or Damaged
            if ((state == FighterState.Dead || state == FighterState.Dying || state == FighterState.Damaged)) return;

            // Process Damage
            currentHealth -= damage;
            scrollerFighter.Damaged();
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
