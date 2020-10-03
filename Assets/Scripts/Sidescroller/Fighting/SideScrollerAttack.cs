using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Fighting;
using Sidescroller.Health;

namespace Sidescroller.Attack
{
    public enum AttackResult
    {
        NoHit,
        NormalHit,
        BlockedHit

    }

    public class SideScrollerAttack : MonoBehaviour
    {
        //Attack
        [SerializeField] float timeBetweenAttacks = .5f;
        float timeSinceLastAttack = Mathf.Infinity;

        public float attackRange = .3f;
        [SerializeField] float attackStrongDamage = 3f;
        [SerializeField] float attackWeakDamage = 1f;

        public Transform attackPos;

        #region Unity Methods

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        #endregion

        public bool CanAttack()
        {
            // Not in the time framed
            if (timeSinceLastAttack < timeBetweenAttacks) {
                return false;
                    }

            // Can Attack
            return true;
        }

        public void AttackBasicHit()
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);//todo mudar pra box quando tiver as sprites certas

            // Update Timer
            timeSinceLastAttack = 0;

            foreach (Collider2D collider in enemiesToDamage)
            {
                if (collider == this.gameObject.GetComponent<Collider2D>()) continue;
                if (collider.GetComponent<SideScrollerHealth>() != null)
                {
                    // Gonna check if the attack is blocked
                    if (collider.GetComponent<SideScrollerFighter>() != null)
                    {
                        if (collider.GetComponent<SideScrollerFighter>().currentState == FighterState.Blocking)
                        {
                            // Attacked Blocked
                            this.gameObject.GetComponent<SideScrollerHealth>().TakeDamage(attackWeakDamage); // Block Damage
                            continue;
                        }
                    }

                    // Hit Player - Deal damage
                    collider.GetComponent<SideScrollerHealth>().TakeDamage(attackStrongDamage);
                }
            }
        }

    public AttackResult AttackHit()
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);//todo mudar pra box quando tiver as sprites certas

            bool hit = false;
            bool blocked = false;

            foreach (Collider2D collider in enemiesToDamage)
            {
                if (collider == this.gameObject.GetComponent<Collider2D>()) continue;
                if (collider.GetComponent<SideScrollerHealth>() != null)
                {
                    // Got a hit
                    hit = true;

                    // Gonna check if the attack is blocked
                    if (collider.GetComponent<SideScrollerFighter>() != null)
                    {
                        if (collider.GetComponent<SideScrollerFighter>().currentState == FighterState.Blocking)
                        {
                            // Attacked Blocked
                            this.gameObject.GetComponent<SideScrollerHealth>().TakeDamage(attackWeakDamage); // Block Damage
                           blocked = true;
                           continue;
                        }
                    }

                    // Hit Player - Deal damage
                    collider.GetComponent<SideScrollerHealth>().TakeDamage(attackStrongDamage);
                }
            }

            // Return result
            if (hit)
            {
                if (blocked){return AttackResult.BlockedHit;}
                else{return AttackResult.NormalHit;}
            }

            else {return AttackResult.NoHit;}
        }
    }
}
