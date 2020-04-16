using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sidescroller.Fighting
{

    public class SideScrollerFighter : MonoBehaviour
    {
        Animator animator;

        //setando a velocidade de ataque
        [SerializeField] float timeBetweenAttacks = .5f;
        float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] float attackRange = .3f;
        [SerializeField] float attackDamage = 1f;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void AttackBasic()
        {
            if (timeSinceLastAttack < timeBetweenAttacks) { return; }
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        private void AttackBasicHit()
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, attackRange);//todo mudar pra box quando tiver as sprites certas
            foreach (Collider2D collider in enemiesToDamage) 
            { 
                if (collider.GetComponent<Health>() != null)
                {
                    collider.GetComponent<Health>().TakeDamage(attackDamage);
                }

            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange); //medir visualmente o attackrange
        }
    }

}