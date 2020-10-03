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

        // Char

        // Delay Variables
        public bool isInDelayTime = false;
        public float thinkDelay = .5f;

        [SerializeField] float thinkDelayMin = .4f;
        [SerializeField] float thinkDelayMax = .9f;
        [SerializeField] float timeSinceDelay = 0f;

        // Action Variables
        [SerializeField] ControllerAction lastAction = ControllerAction.IDLE;
        [SerializeField] ControllerAction nextAction = ControllerAction.IDLE;
        

        // Target Variables 
        [SerializeField] Transform currentTarget = null;
        [SerializeField] float attackDistance = .6f;

        public enum ThinkState
        {
            AlwaysAttack,
            Attack,
            Block,
            Escape
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


            // Check conditions if it can receive Input
            if (!CanReceiveInput())  return; 


            // Check if it is about to performang a action
            if (isInDelayTime)
            {
                // After Delay Time -> Act New action + End Delay
                if (timeSinceDelay > thinkDelay)
                {
                    ProcessAction( nextAction );
                    lastAction = nextAction;
                    ResetDelayTime();
                }

                // Add to Delay time + Perform previus Action
                else
                {
                    timeSinceDelay += Time.deltaTime;
                    ProcessAction(lastAction);
                }
            }


            // Check for possible new actions -> Check new States
            else
            {
                // Think AI state
                currentThinkState = GetCurrentThink();

                // Controler Action
                ControllerAction newAction = GetCurrentAction(currentThinkState);

                //If new action -> Start Delay
                if (newAction != lastAction)
                {
                    nextAction = newAction;
                    ProcessAction(lastAction);

                    // Random Delay Time
                    SetNewDelayTime();

                }

                //Repeat last Action
                else
                {
                    ProcessAction(lastAction);
                }
            }
            
         }
            

        protected void ProcessAction(ControllerAction action)
        {

            if (action == ControllerAction.ATTACK)
            {
                //Try to Attack
                fighterScript.AttackBasic();
                
            }

            else if (action == ControllerAction.WALK_LEFT)
            {
                fighterScript.Walk(-1f);
            }

            else if (action == ControllerAction.WALK_RIGHT)
            {
                fighterScript.Walk(1f);
            }

            else
            {
                SetCharacterToStandStill();
            }
        }

        //Decide mode of operation - !!! TMEP # Only attack
        protected ThinkState GetCurrentThink( )
        {
            return ThinkState.Attack;
        }

        //Decide action based on current state
        protected ControllerAction GetCurrentAction( ThinkState thinkState)
        {
            // Reset action and Delay

            if (thinkState == ThinkState.Attack)
            {
                Vector2 targetDistance = currentTarget.position - transform.position;

                // Debug print print(fighterScript.attackRange.ToString() + "/" + (currentTarget.position - transform.position).magnitude.ToString());
                //if AI is far from Player
                if (fighterScript.GetAttackRange() + (attackDistance) < targetDistance.magnitude)
                {
                    if ( targetDistance.x > 0) return ControllerAction.WALK_RIGHT;

                    else return ControllerAction.WALK_LEFT;
                }

                else return ControllerAction.ATTACK;
            }

            return ControllerAction.IDLE;
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