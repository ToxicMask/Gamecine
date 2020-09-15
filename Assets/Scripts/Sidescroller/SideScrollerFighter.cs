using UnityEngine;
namespace Sidescroller.Fighting
{
    public enum FighterState
    {
        Idle,
        Attacking,
        Blocking,
        Damaged,
        Dead,
        Dying,
        Dodging,
    }
    public class SideScrollerFighter : MonoBehaviour
    {
        Animator animator;
        public FighterState currentState = FighterState.Idle;

        //setando a velocidade de ataque
        [SerializeField] float timeBetweenAttacks = .5f;
        float timeSinceLastAttack = Mathf.Infinity;

        [SerializeField] float attackRange = .3f;
        [SerializeField] float attackDamage = 1f;

        public Transform attackPos;

        public AudioListener audioListener;

        public AudioClip audioDamaged;
        public AudioClip audioAttackGrunt;
        public AudioClip audioAttackHit;
        public AudioClip audioAttackBlock;
        public AudioClip audioDeath;

        private Vector3 startPosition;


        private void Start()
        {
            animator = GetComponent<Animator>();

            startPosition = transform.position;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public void ResetFighterPosition()
        {
            transform.position = startPosition;
        }

        public void ResetAnimator()
        {
            animator.SetFloat("Walk", 0f);
            animator.SetTrigger("Reset");
        }

        public void AttackBasic()
        {
            if (timeSinceLastAttack < timeBetweenAttacks) { return; }

            currentState = FighterState.Attacking;
            animator.SetTrigger("Attack");
            timeSinceLastAttack = 0;
        }

        public void Block()
        {
            currentState = FighterState.Blocking;
            animator.SetTrigger("Block");
        }

        public void Damaged()
        {
            currentState = FighterState.Damaged;
            animator.SetTrigger("Damage");
            if (audioDamaged != null) AudioSource.PlayClipAtPoint( audioDamaged, audioListener.transform.position, 1f);
        }

        public void Death()
        {
            // Dying State ==> Death Animation
            currentState = FighterState.Dying;
            animator.SetTrigger("Death");

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
                            this.gameObject.GetComponent<Health>().TakeDamage(1f); // Block Damage
                            continue;
                        }

                    }
                    // Hit Player
                    if (audioAttackHit != null) AudioSource.PlayClipAtPoint(audioAttackHit, audioListener.transform.position, .2f);
                    collider.GetComponent<Health>().TakeDamage(attackDamage);
                }
            }

        }


        public void ChangeState(FighterState newState) //usado em eventos nos finais das animações pra retornar ao estado de idle;
        {
            currentState = newState;
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawWireSphere(attackPos.position, attackRange);
        }
    }
}