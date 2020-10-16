using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.AI;
using Sidescroller.Movement;
using Sidescroller.Fighting;

namespace Sidescroller.Control
{
    public enum ControllerMode
    {
        SINGLE_PLAYER,
        MULTI_PLAYER,
        AI_PLAYER,
        COUNT, // Meta enum
        NUll = -1, // Meta enum - Always last
    }

    public class SideScrollerController : MonoBehaviour
    {
        public ControllerMode currentMode = ControllerMode.SINGLE_PLAYER;


        [SerializeField] [Range(1, 2)] int playerNumber = 1;
        [SerializeField] bool autoSetAI = false;

        // Components
        protected SideScrollerMover moverScript;
        protected SideScrollerFighter fighterScript;


        // Remove self if is AI Controled, then set Control AI componet
        protected void Awake()
        {
            // Auto - AI Controller
            if (currentMode == ControllerMode.AI_PLAYER && autoSetAI) SetCharacterControllerAsAI();
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            // Auto Get
            moverScript = GetComponent<SideScrollerMover>();
            fighterScript = GetComponent<SideScrollerFighter>();
            
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            // Check conditions if it can receive Input
            if (!CanReceiveInput()) return;

            // Process Input
            ProcessInput(currentMode, playerNumber);
        }

        protected bool CanReceiveInput()
        {
            // Return if game is pause
            if (PauseSystem.gameIsPaused) return false;

            // Return if Character is Acting
            if (fighterScript.currentState == FighterState.Blocking || fighterScript.currentState == FighterState.Attacking) return false;

            // Return if Character is taking Damage, do Knockback then return
            if (IsBeingDamaged()) return false; 

            // Return if Character is Dead
            if (fighterScript.currentState == FighterState.Dead || fighterScript.currentState == FighterState.Dying) return false;

            // If nothing returns, return true
            return true;
        }

        // If is Damaged, the do th knockback
        protected bool IsBeingDamaged()
        {
            if (fighterScript.currentState == FighterState.Damaged)
            {
                moverScript.Knockback(); return true;
            }

            // If nothing returns, return false
            return false;
        }
        
        protected void ProcessInput(ControllerMode mode, int playerID = -1)
        {

            // Tag to get Input Axis
            string playerTag = "";

            if (mode == ControllerMode.SINGLE_PLAYER)
            {
                // Set to Generic Singleplayer 
                playerTag = "";
            }
            else if (mode == ControllerMode.MULTI_PLAYER)
            {
                // Local Multiplayer
                // Player TAG
                playerTag = playerID.ToString();
            }

            // Input Variables
            float vInput = Input.GetAxis("Vertical" + playerTag);    // Vertical Input
            float hInput = Input.GetAxis("Horizontal" + playerTag);  // Horizontal Input
            bool pButton = Input.GetButtonDown("Action Primary" + playerTag); // Primary Button Pressed
            bool sButton = Input.GetButtonDown("Action Secondary" + playerTag);// Secondary Button Pressed

            // Crouching or Walkinh
            if (vInput < 0)
            {
                fighterScript.Crouch(true);
                // TEMP No Attack or Block
                return;
                
            }

            else // Walking
            {
                fighterScript.Crouch(false);

                // Walk
                if (hInput != 0)
                {
                    fighterScript.Walk(hInput);
                }

                // Stand Still
                else
                {
                    SetCharacterToStandStill();
                }


            }


            // Actions - Atack / Defensce

            // Primary Action - Attack
            if (pButton)
            {
                fighterScript.AttackBasic();
                return;
            }

            // Primary Action - Block
            if (sButton)
            {
                fighterScript.Block();
                return;
            }

        }

        public void SetCharacterToStandStill()
        {
            if (fighterScript != null) fighterScript.Walk(0f);
        }

        public void SetPlayerID(int newID)
        {
            playerNumber = newID;
        }

        public void SetCharacterControllerAsAI()
        {
            SideScrollerAIController newControl = gameObject.AddComponent(typeof(SideScrollerAIController)) as SideScrollerAIController;

            newControl.SetPlayerID(playerNumber);
            Destroy(this);
        }
    } }
