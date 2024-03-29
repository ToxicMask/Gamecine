﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameComponents;

using ChickenGameplay.GameManager;
using ChickenGameplay.Chicken;
using ChickenGameplay.Navigate;
using ChickenGameplay.PickUp;
using ChickenGameplay.Score;


namespace ChickenGameplay.Player
{

    /**
        Player character  - Temporary File to disovery the scope of complete feature set


        Objectives:
            -> Get Input: up, down, left, right (press and hold) (Auto Walk)
            -> Set Movement: Auto Walk
            -> Colision: Hit Walls
            -> Pick Objective: Chicken
    **/

    public class SeekerPlayer : MonoBehaviour
    {
        // Position Control Variables
        [SerializeField] AnimationControl2D animControl = null;
        [SerializeField] Rigidbody2D rb2D;
        [SerializeField] PositionSnap pSnap;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;
        [SerializeField] float runSpeed = 1.5f;
        private bool isShooting = false;

        // Check Hit Wall Variables
        float hitWallReach = .15f;

        //Controller
        Vector2 prevMove = Vector2.zero;

        // Expressions
        Vector2 center => transform.position + new Vector3(0, -0.2f);


        // Start is called before the first frame update
        void Start()
        {
            // Auto-Get from the same GameObject
            rb2D = GetComponent<Rigidbody2D>();
            pSnap = GetComponent<PositionSnap>();
            animControl = GetComponent<AnimationControl2D>();

            // Set Start Animation
            animControl.ChangeState("WalkRight");

        }

        // Update is called once per frame
        void Update()
        {
            // End Update If not In Update
            if (ChickenLevelManager.instance.currentState != GAME_STATE.UPDATE) return;

            // End Update if Character is in shooting animation
            if (isShooting) return;

            // Control Standard
            Vector2 moveInput = new Vector2
                (
                    Mathf.Round(Input.GetAxis("Horizontal")),
                    Mathf.Round(Input.GetAxis("Vertical"))
                );
            //bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
            //bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed


            //Process Input - Movement
            if (moveInput != Vector2.zero &&  moveInput != prevMove)
            {
                pSnap.Snap();
                ChangeRunDirection(moveInput.x, moveInput.y);
            }

            // Record Prev Input
            prevMove = moveInput;

            // Check each Direction
            RaycastHit2D hitWall = Physics2D.Raycast(center, runDirection, hitWallReach);

            if (hitWall)
            {
                if (hitWall.collider.GetComponent<Wall>())
                {
                    runDirection = Vector2.zero;

                    if (pSnap) pSnap.Snap();
                }
            }


            // Execute Movement
            RunMovement();

        }

        private void ResumeMovement()
        {
            isShooting = false;
            RunMovement();
        }

        /**  
         * Changes Current Direction
         * Only Moves in Vertical or Horizontal
         **/
        private void ChangeRunDirection(float hInput, float vInput )
        {
            // Change Run Direction
            // Move vertical
            if (vInput != 0)
            {
                runDirection.x = 0;
                runDirection.y = vInput / Mathf.Abs(vInput);
            }

            // Move Horizontal
            else if (hInput != 0)
            {
                runDirection.x = hInput / Mathf.Abs(hInput);
                runDirection.y = 0;
            }
        }

        /**  
         * Execute Run Movement
         **/
        private void RunMovement()
        {
            // Update position -> Auto Walk
            rb2D.position += runDirection * runSpeed * Time.deltaTime;

            // Update Animation 
            if      (runDirection == Vector2.up) animControl.ChangeState("WalkUp");
            else if (runDirection == Vector2.down) animControl.ChangeState("WalkDown");
            else if (runDirection == Vector2.left) animControl.ChangeState("WalkLeft");
            else if (runDirection == Vector2.right) animControl.ChangeState("WalkRight");
            else if (runDirection == Vector2.zero) animControl.ChangeState("Idle");
        }


        /**
         * Collision Pick Chicken
         *
         **/
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Objective
            RunnerChicken chicken = collision.collider.GetComponent<RunnerChicken>();
            if (chicken)
            {

                // Chicken Pick
                chicken.PickUp();

                // Set Result
                GAME_STATE gameResult = GAME_STATE.VICTORY;

                // Set Result in Manager
                ChickenLevelManager.instance.ChangeState(gameResult);
            }

           
        }

        //Pick Bullet
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BulletPickUp bullet = collision.GetComponent<BulletPickUp>();
            if (bullet)
            {
                bullet.PickUp();

                // Update Animation and State / Delay Movement
                animControl.ChangeState("Shoot");
                isShooting = true;
                Invoke("ResumeMovement", animControl.GetStateLenght());
            }

            EggPickUp egg = collision.GetComponent<EggPickUp>();
            if (egg)
            {
                egg.PickUp();
            }

        }


    }

}
