using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Fighting;
using Sidescroller.Control;
using Sidescroller.Status;

namespace Sidescroller.AI
{
    public class SideScrollerAIController : SideScrollerController
    {

        #region Class Variables

        // AI variable
        ThinkState currentThinkState = ThinkState.Attack;

        // Variation in Noise for random choice
        [SerializeField] float perlinScale = 8;
        private float noiseOffset = 0;

        // Delay Variables
        private bool isInDelayTime = false;
        private float thinkDelay = .5f;

        [SerializeField] float  thinkDelayMin = .1f;
        [SerializeField] float thinkDelayMax = .6f;
        private float timeSinceDelay = 0f;

        // Action Variables
        [SerializeField] FighterState lastState = FighterState.Idle;
        [SerializeField] FighterState nextState = FighterState.Idle;


        // Think variables
        [SerializeField] float evadeLimit = 18f;
        [SerializeField] float defenseLimit = 36f;
        [SerializeField] float attackLimit = 74f;


        // Target Variables 
        [SerializeField] Transform currentTarget = null;
        private float attackThreshold = 0.2f;

        #endregion

        public enum ThinkState
        {
            AlwaysAttack,
            Attack,
            Defensive,
            Escape,
            Confused,
            Evasive,
        }

        //Set Delay Profile
        public void SetDataProfile(AIFighterData profile)
        {
            // Perlin Noise Variables
            this.perlinScale = profile.perlinScale;

            // Delay Variables
            this.thinkDelayMin = profile.thinkDelayMin;
            this.thinkDelayMax = profile.thinkDelayMax;

            // Distance Variation To Attack
            this.attackThreshold = profile.attackThreshold;

            // Variables
            this.evadeLimit = profile.evadeLimit;
            this.defenseLimit = profile.defenseLimit;
            this.attackLimit = profile.attackLimit;

         }
    


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // Set Controller to AI
            currentMode = ControllerMode.AI_PLAYER;

            if (currentTarget == null) {

                if (gameObject.name == "Player1") currentTarget = GameObject.Find("Player2").transform;

                if (gameObject.name == "Player2") currentTarget = GameObject.Find("Player1").transform;
            }

            // Set noise Offset
            noiseOffset = Random.Range(-10000, 10000);
        }


        // Update is called once per frame
        protected override void Update()
        {


            // if fighter last action is diferent, than Update
            if (fighterScript.currentState != lastState) lastState = fighterScript.currentState;

            // Check conditions if it can receive Input
            if (!CanReceiveInput())  return; 


            // Check if it is about to performang a action
            if (isInDelayTime)
            {
                // After Delay Time -> Act New action + End Delay
                if (timeSinceDelay > thinkDelay)
                {
                    ProcessAction(nextState);
                    lastState = nextState;
                    ResetDelayTime();
                }

                // Add to Delay time + Perform previus Action
                else
                {
                    timeSinceDelay += Time.deltaTime;
                    ProcessAction(lastState);
                }
            }


            // Check for possible new actions -> Check new States
            else
            {
                // Think AI state
                currentThinkState = GetCurrentThink();

                // Controler Action
                FighterState newAction = GetCurrentAction(currentThinkState);

                //If new action -> Start Delay
                if (newAction != lastState)
                {
                    nextState = newAction;
                    ProcessAction(lastState);

                    // Random Delay Time
                    SetNewDelayTime();

                }

                //Repeat last Action
                else
                {
                    ProcessAction(lastState);
                }
            }
            
         }
            

        protected void ProcessAction(FighterState action)
        {
            //Crouch
            if (action == FighterState.Crouching)
            {
                fighterScript.Crouch(true);
                return;
            }
            else
            {
                fighterScript.Crouch(false);
            }

            if (action == FighterState.Attacking) { 
                //Try to Attack
                fighterScript.AttackBasic();
                
            }

            else if (action == FighterState.WalkLeft)
            { 
                // Try to walk
                fighterScript.Walk(-1f);
            }

            else if (action == FighterState.WalkRight)
            {
                // Try to walk
                fighterScript.Walk(1f);
            }

            else if (action == FighterState.Blocking)
            {
                // Try to walk
                fighterScript.Block();
            }

            else if (action == FighterState.Crouching)
            {
                fighterScript.Crouch(true);
            }

            else
            {
                // Try to walk
                SetCharacterToStandStill();
            }
        }

        //Decide mode of operation 
        protected ThinkState GetCurrentThink( )
        {
            // Result between 0 - 100 - Smoth variation
            float rF = Mathf.PerlinNoise((Time.time * perlinScale) +  noiseOffset, 0)  * 100 ;


            // Evade
            if (rF < evadeLimit) return ThinkState.Evasive;

            //  Defesive
            else if (rF < defenseLimit) return ThinkState.Defensive;

            // Attack
            else if (rF < attackLimit) return ThinkState.Attack;

            // Escape
            else return ThinkState.Escape;
        }

        //Decide action based on current state
        protected FighterState GetCurrentAction( ThinkState thinkState)
        {
            // Report Current  Think State
            //print(thinkState.ToString());

            // Wants to attack
            if (thinkState == ThinkState.Attack)
            {
                Vector2 targetDistance = currentTarget.position - transform.position;

                // Debug print print(fighterScript.attackRange.ToString() + "/" + (currentTarget.position - transform.position).magnitude.ToString());
                //if AI is far from Player
                if (fighterScript.GetAttackRange() - (attackThreshold) <= targetDistance.magnitude)
                {
                    if ( targetDistance.x > 0) return FighterState.WalkRight;

                    else return FighterState.WalkLeft;
                }

                else return FighterState.Attacking;
            }

            else if (thinkState == ThinkState.Escape)
            {

                Vector2 targetDistance = currentTarget.position - transform.position;

                // Try to Escape
                if (targetDistance.x < 0) return FighterState.WalkRight;

                else return FighterState.WalkLeft;
            }

            else if (thinkState == ThinkState.Defensive)
            {

                Vector2 targetDistance = currentTarget.position - transform.position;

                // If close
                if (fighterScript.GetAttackRange() - (attackThreshold) > targetDistance.magnitude)
                {
                    return FighterState.Blocking;
                }

                return FighterState.Idle;
            }

            else if (thinkState == ThinkState.Evasive)
            {
                Vector2 targetDistance = currentTarget.position - transform.position;

                // If close
                if (fighterScript.GetAttackRange() - (attackThreshold) > targetDistance.magnitude)
                {
                    return FighterState.Crouching;
                }

                return FighterState.Idle;
            }

            // If nothing else, then satd put
            return FighterState.Idle;
        }

        #region Delay Methods

        // Reset
        protected void ResetDelayTime()
        {
            isInDelayTime = false;
            timeSinceDelay = 0f;
        }

        protected void SetNewDelayTime()
        {
            // Random Delay Time
            isInDelayTime = true;
            thinkDelay = Random.Range(thinkDelayMin, thinkDelayMax);
        }

        protected void SetNewDelayTime(float time)
        {
            // Random Delay Time
            isInDelayTime = true;
            thinkDelay = time;
        }

        #endregion
    }
}