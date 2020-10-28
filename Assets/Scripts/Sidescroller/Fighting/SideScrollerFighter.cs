using UnityEngine;
using Sidescroller.Attack;
using Sidescroller.Health;
using Sidescroller.Pose;
using Sidescroller.Movement;
using Sidescroller.Animation;
using Sidescroller.Audio;
using Sidescroller.Status;

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
        private SideScrollerAnimation animationScript = null;  // Animation
        private SideScrollerAttack attackScript = null;        // Attack
        private SideScrollerAudio audioScript = null;          // Audio
        private SideScrollerHealth healthScript = null;        // Health
        private SideScrollerMover moverScript = null;          // Movement
        private SideScrollerPose poseScript = null;            // Pose

        // Scriptable objects - Fighter stats
        [SerializeField] FighterStatus statusData = null;

        // Self Position
        private Vector3 startPosition;

        #region Unity Methods

        private void Awake()
        {
            animationScript = GetComponent<SideScrollerAnimation>();
            audioScript = GetComponent<SideScrollerAudio>();
            attackScript = GetComponent<SideScrollerAttack>();
            healthScript = GetComponent<SideScrollerHealth>();
            moverScript = GetComponent<SideScrollerMover>();
            poseScript = GetComponent<SideScrollerPose>();

            startPosition = transform.position;

            // Set Status - if has preset
            if (statusData) SetFighterData(statusData);

        }

        #endregion

        // Set Fighters Stats

        private void SetFighterData(FighterStatus data)
            { 

                // Attack
                float TBA = data.timeBetweenAttacks;
                float MAR = data.attackRange;
                float ASD = data.attackStrongDamage;
                float AWD = data.attackWeakDamage;

                // Update
                if (attackScript) attackScript.SetStatus(TBA, MAR, ASD, AWD);

                // Health
                float MX = data.maxHealth;

                // Update
                if (healthScript) healthScript.SetMaxHealth(MX);

                // Movement
                float walkSpeed = data.walkSpeed;
                float knockSpeed = data.knockbackSpeed;

                // Update
                if (moverScript) moverScript.SetStatus(walkSpeed, knockSpeed);
            }

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
            audioScript.PlayClip(AudioClips.AttackGrunt);

        }

        public void Block()
        {
            ChangeState(FighterState.Blocking);
            
        }

        public void Damaged() // Comes from health Script
        {
            ChangeState(FighterState.Damaged);
            audioScript.PlayClip(AudioClips.Damaged);
            //if (audioDamaged != null) AudioSource.PlayClipAtPoint( audioDamaged, audioListener.transform.position, 1f);
            
        }

        public void Death()
        {
            // Dying State ==> Death Animation
            ChangeState(FighterState.Dying);

            // Disable Animation Changes
            EnableAnimationChange(false);

            // Audio
            audioScript.PlayClip(AudioClips.Death);
            //if (audioDeath != null) AudioSource.PlayClipAtPoint(audioDeath, audioListener.transform.position, 1f);
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
                    audioScript.PlayClip(AudioClips.AttackHit, .2f);
                    //if (audioAttackHit != null) AudioSource.PlayClipAtPoint(audioAttackHit, audioListener.transform.position, .2f);
                    break;

                case AttackResult.BlockedHit:
                    audioScript.PlayClip(AudioClips.AttackBlocked, .8f);
                    audioScript.PlayClip(AudioClips.Damaged, .6f);
                    //if (audioAttackBlock != null) AudioSource.PlayClipAtPoint(audioAttackBlock, audioListener.transform.position, .8f);
                    //if (audioDamaged != null) AudioSource.PlayClipAtPoint(audioDamaged, audioListener.transform.position, .6f);
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

            // TEmp
            if (gameObject.name == "Player1")
            {
                //print(animationScript.GetCurrentAnimationLenght());
            }
            
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
            return attackScript.GetAttackRange();
        }

        public float GetAnimationLenght()
        {
            return animationScript.GetCurrentAnimationLenght();
        }

        #endregion

    }
}