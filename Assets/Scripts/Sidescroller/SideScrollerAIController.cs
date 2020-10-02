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
        ThinkState currentState = ThinkState.Attack;

        // Delay Variables
        protected bool isInDelayTime = false;
        protected float thinkDelay = .5f;

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
                    isInDelayTime = false;
                    timeSinceDelay = 0f;
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
                currentState = GetCurrentThink();

                // Controler Action
                ControllerAction newAction = GetCurrentAction(currentState);

                //If new action -> Start Delay
                if (newAction != lastAction)
                {
                    isInDelayTime = true;
                    nextAction = newAction;
                    ProcessAction(lastAction);

                    // Random Delay Time
                    thinkDelay = Random.Range(thinkDelayMin, thinkDelayMax);

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
                // Debug print print(fighterScript.attackRange.ToString() + "/" + (currentTarget.position - transform.position).magnitude.ToString());
                //if AI is far from Player
                if (fighterScript.attackRange + (attackDistance) < (currentTarget.position - transform.position).magnitude)
                {
                    
                    return ControllerAction.WALK_LEFT;
                }

                else return ControllerAction.ATTACK;
            }

            return ControllerAction.IDLE;
        }
    }
}