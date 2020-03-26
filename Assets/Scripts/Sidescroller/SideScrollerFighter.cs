using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sidescroller.Fighting
{

    public class SideScrollerFighter : MonoBehaviour
    {
        [SerializeField] Animator animator;

        public void AttackBasic()
        {
                animator.SetTrigger("Attack");
        }
    }
}