using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.GameManager;
using ChickenGameplay.Navigate;


namespace ChickenGameplay.Chicken
{
    public class RunnerChicken : MonoBehaviour
    {

        public static RunnerChicken current = null;

        // Components
        [SerializeField] Rigidbody2D rb2D;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;

        //Navigation Componets
        DirectionGuide lastDirectionGuide = null;

        //Movement Values
        public float runSpeed = 1.2f;
        float hitWallReach = .15f;

        // Egg
        public GameObject eggPrefab;
        public Transform eggFolder;

        // Timers
        public float eggTimerValue = 1.0f;
        private Timer eggTimer;
        private Timer stunTimer;


        // Stun
        public bool stuned = false;

        void Start()
        {
            // Set current static reference
            current = this;

            // Set Timer
            eggTimer = new Timer(eggTimerValue);
            eggTimer.OnComplete += SpawnEgg;
            stunTimer = new Timer(1f);
            stunTimer.OnComplete += EndStun;

            // Auto-Get from the same GameObject
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();

        }
        void Update()
        {

            // End Update If not In Update
            if (ChickenLevelManager.instance.currentState != GAME_STATE.UPDATE) return;


            //Update Timers
            eggTimer.Update();
            if (stuned) stunTimer.Update();
            
            // Return if stunned
            if (stuned) return;

            // Check each Direction
            RaycastHit2D hitWall = Physics2D.Raycast(transform.position, runDirection, hitWallReach);
            if (hitWall)
            {
                if (hitWall.collider.GetComponent<Wall>())
                {
                    //Get new Direction // Invert Direction
                    ChangeRunDirection(-runDirection);
                }
            }

            // Execute Movement
            RunMovement();

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            // Check New Direction Chenge to horizontal to vertical and vice-versa
            DirectionGuide dg = collision.GetComponent<DirectionGuide>();

            if ((dg) && dg != lastDirectionGuide )
            {
                // Get Distance
                float distance = (transform.position - dg.transform.position).magnitude;
                float threshold = .1f;

                if (distance <= threshold)
                {
                    // Eliminate Theshhold
                    transform.position = collision.transform.position;

                    // Set New Direction
                    Vector2 newDirection = dg.GetSetDirection(runDirection);

                    ChangeRunDirection(newDirection);

                    //Update Navigation
                    lastDirectionGuide = dg;
                }
            }

        }
        private void OnDestroy()
        {
            current = null;
        }

        /**  
         * Changes Current Direction
         * Only Moves in Vertical or Horizontal
         **/
        public void ChangeRunDirection(Vector2 newDir)
        {
            // Change Run Direction
            runDirection = newDir;
        }


        /**  
         * Execute Run Movement
         **/
        private void RunMovement()
        {
            // Update position -> Auto Walk
            rb2D.position += runDirection * runSpeed * Time.deltaTime;
        }

        /**
        *   Signals
        **/

        // Spawn Egg and Reset Timer
        void SpawnEgg()
        {
            if (eggPrefab)
            {
                if (eggFolder) GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero), eggFolder);
                else GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero));
            }

            eggTimer.Reset();
        }
        public void SetStunTime(float stunTime )
        {
            stuned = true;
            stunTimer.setTime = stunTime;
        }
        // End Stunned Condition
        void EndStun()
        {
            stuned = false;
            stunTimer.Reset();
        }
    }
}
