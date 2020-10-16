using UnityEngine;
using Sidescroller.Attack;
using Sidescroller.Health;
using Sidescroller.Pose;
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

        Crouching,
        CrouchingAttack,
        CrouchingBlock,
    }

    public class SideScrollerFighter : MonoBehaviour
    {

        public FighterState currentState = FighterState.Idle;


        // Sidescroller Scripts
        [SerializeField] SideScrollerAnimation animationScript = null;  // Animation
        [SerializeField] SideScrollerMover moverScript = null;          // Movement
        [SerializeField] SideScrollerAttack attackScript = null;        // Attack
        [SerializeField] SideScrollerPose poseScript = null;            // Pose

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
            poseScript = GetComponent<SideScrollerPose>();

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
                ChangeState(FighterState.Idle);
            }

            else
            {
                // Walking

                if (direction > 0f)
                {
                    ChangeState(FighterState.WalkRight);
                }
                else if (direction < 0f)

                {
                    ChangeState(FighterState.WalkLeft);
                }
            }
        }

        public void Crouch(bool crouchCommand)
        {
            bool isCrouching = poseScript.IsCrouching();

            //Update Pose
            if (crouchCommand)
            {
                if (!poseScript.IsCrouching()) poseScript.Crouch();

                // Update Animation
                ChangeState(FighterState.Crouching);
            }

            else
            {
                if (poseScript.IsCrouching()) poseScript.StandUp();
            }



        }

        public void AttackBasic()
        {
            if (!attackScript.CanAttack()) { return; }

            // Action + Animation
            // Decide Attacking
            ChangeState(FighterState.Attacking);

            // Battle Cry - Audio
            if (audioAttackGrunt != null)
            {
                AudioSource.PlayClipAtPoint(audioAttackGrunt, audioListener.transform.position, .6f);
            }
           
        }

        public void Block()
        {
            ChangeState(FighterState.Blocking);
            
        }

        public void Damaged()
        {
            ChangeState(FighterState.Damaged);
            if (audioDamaged != null) AudioSource.PlayClipAtPoint( audioDamaged, audioListener.transform.position, 1f);
            
        }

        public void Death()
        {
            // Dying State ==> Death Animation
            ChangeState(FighterState.Dying);

            // Disable Animation Changes
            EnableAnimationChange(false);

            // Audio
            if (audioDeath != null) AudioSource.PlayClipAtPoint(audioDeath, audioListener.transform.position, 1f);
        }

        #endregion

        // Method for Attack Animation
        public void ProcessAttackResult()
        {
            // Find fi is Crouching
            bool Crouch = poseScript.IsCrouching(); 

            //Attack in script and return Value
            AttackResult attackResult = attackScript.AttackHit( Crouch );

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

        public void ChangeStateToIdle()
        {
            currentState = FighterState.Idle;

            //Update Collision Box
            poseScript.StandUp();

            // Animation Set
            animationScript.ChangeAnimationState(currentState);
        }

        public void ChangeStateToDead()
        {
            currentState = FighterState.Dead;

            //Update Collision Box
            poseScript.StandUp();

            // Animation Set
            animationScript.ChangeAnimationState(currentState);
        }

        public void ChangeState(FighterState newState) //usado em eventos nos finais das animações pra retornar ao estado de idle;
        {
            currentState = newState;

            // Animation Set
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

        #endregion

        #region Check Methods

        public float GetAttackRange()
        {
            return attackScript.attackRange;
        }

        public float GetAnimationLenght()
        {
            return animationScript.GetCurrentAnimationLenght();
        }

        #endregion

    }
}