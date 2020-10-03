using UnityEngine;
using Sidescroller.Attack;
using Sidescroller.Health;
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


        // Sidescroller Scripts
        [SerializeField] SideScrollerAnimation animationScript = null; // Animation
        [SerializeField] SideScrollerMover moverScript = null; // Movement
        [SerializeField] SideScrollerAttack attackScript = null; //Attack

        // Audio Variables
        public AudioListener audioListener;

        public AudioClip audioDamaged;
        public AudioClip audioAttackGrunt;
        public AudioClip audioAttackHit;
        public AudioClip audioAttackBlock;
        public AudioClip audioDeath;

        private Vector3 startPosition;

        #region Unity Methods

        private void Awake()
        {
            animationScript = GetComponent<SideScrollerAnimation>();
            attackScript = GetComponent<SideScrollerAttack>();
            moverScript = GetComponent<SideScrollerMover>();

            startPosition = transform.position;
        }

        #endregion

        // Action Methods - Input Commands
        #region Action Methods - Commands

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
            if (!attackScript.CanAttack()) { return; }

            // Action + Animation
            print(gameObject.name);
            currentState = FighterState.Attacking;
            animationScript.ChangeAnimationState(currentState);

            // Battle Cry - Audio
            if (audioAttackGrunt != null)
            {
                AudioSource.PlayClipAtPoint(audioAttackGrunt, audioListener.transform.position, .6f);
            }
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

        #endregion

        // Method for Attack Animation
        public void ProcessAttackResult()
        {
            AttackResult attackResult = attackScript.AttackHit();

            // Play sound
            switch (attackResult)
            {
                case AttackResult.NoHit:
                    break;

                case AttackResult.NormalHit:
                    if (audioAttackHit != null) AudioSource.PlayClipAtPoint(audioAttackHit, audioListener.transform.position, .2f);
                    break;

                case AttackResult.BlockedHit:
                    if (audioAttackBlock != null) AudioSource.PlayClipAtPoint(audioAttackBlock, audioListener.transform.position, .8f);
                    if (audioDamaged != null) AudioSource.PlayClipAtPoint(audioDamaged, audioListener.transform.position, .6f);
                    break;
            }
            
        }

        #region Action Methods - Others

        public void ChangeState(FighterState newState) //usado em eventos nos finais das animações pra retornar ao estado de idle;
        {
            currentState = newState;

            // Animation Reset
            animationScript.ChangeAnimationState(currentState);
            print(gameObject.name + newState.ToString());
        }

        public void EnableAnimationChange(bool active)
        {
            animationScript.canChangeAnimation = active;
        }

        public void ResetFighterPosition()
        {
            transform.position = startPosition;
        }

        #endregion

        #region Methods
        public float GetAttackRange()
        {
            return attackScript.attackRange;
        }

        #endregion

    }
}