using UnityEngine;

using Sidescroller.Movement;
using Sidescroller.Animation;

namespace Sidescroller.Fighting
{
    public enum FighterState
    {
        Idle,
        WalkLeft,
        WalkRight,
        Attacking,
        Blocking,
        Damaged,
        Dead,
        Dying,
    }

    public class SideScrollerFighter : MonoBehaviour
    {

        public FighterState currentState = FighterState.Idle;

        // Animation
        [SerializeField] SideScrollerAnimation animationScript = null;

        // Movement
        [SerializeField] SideScrollerMover moverScript = null;

        //Attack
        [SerializeField] float timeBetweenAttacks = .5f;
        float timeSinceLastAttack = Mathf.Infinity;

        public float attackRange = .3f;
        [SerializeField] float attackStrongDamage = 3f;
        [SerializeField] float attackWeakDamage = 1f;

        public Transform attackPos;

        // Audio Variables
        public AudioListener audioListener;

        public AudioClip audioDamaged;
        public AudioClip audioAttackGrunt;
        public AudioClip audioAttackHit;
        public AudioClip audioAttackBlock;
        public AudioClip audioDeath;

        private Vector3 startPosition;


        private void Awake()
        {
            animationScript = GetComponent<SideScrollerAnimation>();
            moverScript = GetComponent<SideScrollerMover>();

            startPosition = transform.position;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }


        // Action Methods - Input Commands

        public void Walk(float direction)
        {

            // Action 
            moverScript.Walk(direction);

            // Idle 
            if (direction == 0f)
            {
                currentState = FighterState.Idle;
                animationScript.ChangeAnimationState(currentState);
            }

            else
            {
                // Walking

                if (direction > 0f)
                {
                    currentState = FighterState.WalkRight;
                    animationScript.ChangeAnimationState(currentState);
                }
                else if (direction < 0f)

                {
                    currentState = FighterState.WalkLeft;
                    animationScript.ChangeAnimationState(currentState);
                }
            }
        }

        public void AttackBasic()
        {
            if (timeSinceLastAttack < timeBetweenAttacks) { return; }

            currentState = FighterState.Attacking;
            animationScript.ChangeAnimationState(currentState);
            timeSinceLastAttack = 0;
        }

        public void Block()
        {
            currentState = FighterState.Blocking;
            animationScript.ChangeAnimationState(currentState);
        }

        public void Damaged()
        {
            currentState = FighterState.Damaged;
            animationScript.ChangeAnimationState(currentState);
            if (audioDamaged != null) AudioSource.PlayClipAtPoint( audioDamaged, audioListener.transform.position, 1f);
        }

        public void Death()
        {
            // Dying State ==> Death Animation
            currentState = FighterState.Dying;
            animationScript.ChangeAnimationState(currentState);

            // Disable Animation Changes
            EnableAnimationChange(false);



            if (audioDeath != null) AudioSource.PlayClipAtPoint(audioDeath, audioListener.transform.position, 1f);
        }

        private void AttackBasicHit()
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange);//todo mudar pra box quando tiver as sprites certas

            // Battle Cry
            if ( audioAttackGrunt != null) { AudioSource.PlayClipAtPoint(audioAttackGrunt, audioListener.transform.position, .6f);}

            foreach (Collider2D collider in enemiesToDamage)
            {
                if (collider == this.gameObject.GetComponent<Collider2D>()) continue;
                if (collider.GetComponent<Health>() != null)
                {
                    if (collider.GetComponent<SideScrollerFighter>() != null)
                    {
                        if (collider.GetComponent<SideScrollerFighter>().currentState == FighterState.Blocking)
                        {
                            // Attacked Blocked
                            if (audioAttackBlock != null) AudioSource.PlayClipAtPoint(audioAttackBlock, audioListener.transform.position, .8f);
                            if (audioDamaged != null) AudioSource.PlayClipAtPoint(audioDamaged, audioListener.transform.position, .6f);
                            this.gameObject.GetComponent<Health>().TakeDamage(attackWeakDamage); // Block Damage
                            continue;
                        }

                    }
                    // Hit Player
                    if (audioAttackHit != null) AudioSource.PlayClipAtPoint(audioAttackHit, audioListener.transform.position, .2f);
                    collider.GetComponent<Health>().TakeDamage(attackStrongDamage);
                }
            }

        }


        public void ChangeState(FighterState newState) //usado em eventos nos finais das animações pra retornar ao estado de idle;
        {
            currentState = newState;

            // Animation Reset
            animationScript.ChangeAnimationState(currentState);
        }

        public void EnableAnimationChange(bool active)
        {
            animationScript.canChangeAnimation = active;
        }

        public void ResetFighterPosition()
        {
            transform.position = startPosition;
        }
    }
}