using Sidescroller.Fighting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sidescroller.Health
{

    public class SideScrollerHealth : MonoBehaviour
    {
        // Components
        public PlayerHealthBar playerBar;

        //Variables
        float maxHealth = 10;
        float currentHealth;

        void Start()
        {
            currentHealth = maxHealth;

            // Update player Bar
            if (playerBar != null)
            {
                playerBar.SetHealth((int)currentHealth);
                playerBar.SetMaxHealth((int)maxHealth);
            }
        }

        public void SetMaxHealth(float newValue)
        {
            // Set new Value
            this.maxHealth = newValue;

            // Update - Health + UI
            FillToMaxHealth();
        }

        public void FillToMaxHealth()
        {
            currentHealth = maxHealth;
            // Display Damage // Update UI
            if (playerBar)
            {
                playerBar.SetHealth((int)currentHealth);
                playerBar.SetMaxHealth((int)maxHealth);
            }
        }

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

                // Debug PRINTS
                //print(this.name + " took damage");
                //print(currentHealth);
            }
        }

        void Die()
        {
            // Display Damage // Update UI
            if (playerBar != null) playerBar.SetHealth(0);

            //this.GetComponent<SideScrollerFighter>().ChangeState(FighterState.Dead);
            this.GetComponent<SideScrollerFighter>().Death();
        }
    }
}
