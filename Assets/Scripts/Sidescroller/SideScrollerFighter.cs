using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sidescroller.Fighting
{

    public class SideScrollerFighter : MonoBehaviour
    {
        Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void AttackBasic()
        {
                animator.SetTrigger("Attack");
        }
    }
}