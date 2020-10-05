using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Movement;
using Sidescroller.Fighting;
using Sidescroller.Control;

namespace Sidescroller.AI
{
    public class SideScrollerAIController : SideScrollerController
    {
        // AI variable
        ThinkState currentThinkState = ThinkState.Attack;

        // 

        // Delay Variables
        public bool isInDelayTime = false;
        public float thinkDelay = .5f;

        [SerializeField] float thinkDelayMin = .1f;
        [SerializeField] float thinkDelayMax = .9f;
        [SerializeField] float timeSinceDelay = 0f;

        // Action Variables
        [SerializeField] FighterState lastState = FighterState.Idle;
        [SerializeField] FighterState nextState = FighterState.Idle;
        

        // Target Variables 
        [SerializeField] Transform currentTarget = null;
        [SerializeField] float attackDistance = .6f;

        public enum ThinkState
        {
            AlwaysAttack,
            Attack,
            Defensive,
            Escape,
            Confused,
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

            if (action == FighterState.Attacking) { 
                //Try to Attack
                fighterScript.AttackBasic();
                
            }

            else if (action == FighterState.WalkLeft)
            {
                fighterScript.Walk(-1f);
            }

            else if (action == FighterState.WalkRight)
            {
                fighterScript.Walk(1f);
            }

            else if (action == FighterState.Blocking)
            {
                fighterScript.Block();
            }

            else
            {
                SetCharacterToStandStill();
            }
        }

        //Decide mode of operation 
        protected ThinkState GetCurrentThink( )
        {
            // Result between 1 - 100
            int rF = Random.Range((int)1, (int)100);


            // 10% - Escape
            if (rF < 10) return ThinkState.Escape;

            // 5 % - Defesive
            else if (rF < 15) return ThinkState.Defensive;

            // 57.5% - Attack
            else if (rF < 97.5f) return ThinkState.Attack;

            // 5% - Confused
            else return ThinkState.Confused;
        }

        //Decide action based on current state
        protected FighterState GetCurrentAction( ThinkState thinkState)
        {
            // Report Current  Think State
            print(thinkState.ToString());

            // Wants to attack
            if (thinkState == ThinkState.Attack)
            {
                Vector2 targetDistance = currentTarget.position - transform.position;

                // Debug print print(fighterScript.attackRange.ToString() + "/" + (currentTarget.position - transform.position).magnitude.ToString());
                //if AI is far from Player
                if (fighterScript.GetAttackRange() + (attackDistance) < targetDistance.magnitude)
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

                if (fighterScript.GetAttackRange() + (attackDistance) > targetDistance.magnitude)
                {
                    return FighterState.Blocking;
                }

                return FighterState.Idle;
            }


            // If nothing else, then satd put
            return FighterState.Idle;
        }

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
    }
}