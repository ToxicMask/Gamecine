using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenPrototype.Chicken;
using ChickenPrototype.Navigate;


namespace ChickenPrototype.Player
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
        [SerializeField] Rigidbody2D rb2D;
        [SerializeField] PositionSnap pSnap;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;
        [SerializeField] float runSpeed = 1.5f;

        // Check Hit Wall Variables
        float hitWallReach = .15f;

        //Controller
        Vector2 prevMove = Vector2.zero;


        // Start is called before the first frame update
        void Start()
        {
            // Auto-Get from the same GameObject
            rb2D = GetComponent<Rigidbody2D>();
            pSnap = GetComponent<PositionSnap>();

        }

        // Update is called once per frame
        void Update()
        {
            // Control Standard
            Vector2 moveInput = new Vector2
                (
                    Mathf.Round(Input.GetAxis("Horizontal")),
                    Mathf.Round(Input.GetAxis("Vertical"))
                );
            bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
            bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed


            // Process Input - Action
            if (pButton)
            {
                print("Shoot");
                pSnap.Snap();
                return;
            }

            //Process Input - Movement
            if (moveInput != Vector2.zero &&  moveInput != prevMove)
            {
                pSnap.Snap();
                ChangeRunDirection(moveInput.x, moveInput.y);
            }

            // Record Prev Input
            prevMove = moveInput;

            // Check each Direction
            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, runDirection, hitWallReach);

            if (hitWall)
            {
                if (hitWall.collider.GetComponent<Wall>())
                {
                    runDirection = Vector2.zero;//Stop

                    if (pSnap) pSnap.Snap();
                }
            }


            // Execute Movement
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
        }

    }

}
