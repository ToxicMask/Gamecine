using Sidescroller.Fighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{

    public PlayerHealthBar playerBar;

    [SerializeField] float maxHealth = 10;
    public float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;

        // Update player Bar
        if (playerBar != null) playerBar.SetHealth((int)currentHealth);
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

            // Display Damage // Update UI
            if (playerBar != null) playerBar.SetHealth((int)currentHealth);


            print(this.name + " took damage");
            print(currentHealth);
        }
    }
    void Die()
    {
        // Display Damage // Update UI
        if (playerBar != null) playerBar.SetHealth(0);

        //this.GetComponent<SideScrollerFighter>().ChangeState(FighterState.Dead);
        this.GetComponent<SideScrollerFighter>().Death();
        this.GetComponent<Collider2D>().enabled = false;
    }
}
