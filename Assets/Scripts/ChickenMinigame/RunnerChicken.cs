using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameComponents;

using ChickenGameplay.GameManager;
using ChickenGameplay.Navigate;
using ChickenGameplay.Score;


namespace ChickenGameplay.Chicken
{
    public class RunnerChicken : MonoBehaviour
    {

        public static RunnerChicken current = null;

        // Components
        private AnimationControl2D animControl = null;
        private Rigidbody2D rb2D;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;

        //Navigation Componets
        DirectionGuide lastDirectionGuide = null;

        //Movement Values
        public float runSpeed = 1.2f;
        float hitWallReach = .15f;

        // Sounds
        [Header("Status")]
        [SerializeField] AudioClip pickSFX = null;

        // Stun
        [Header("Status")]
        public bool stuned = false;

        // Egg
        [Header("Egg")]
        [SerializeField] GameObject eggPrefab = null;
        public Transform eggFolder = null;
        [SerializeField] AudioClip eggSFX = null;

        // Timers
        public float eggTimerValue = 1.0f;
        private Timer eggTimer;
        private Timer stunTimer;

        // Score
        [Header("Score")]
        public float currentPickScore = 0;
        public float maxScore = 5000;
        public float minScore = 1000;
        public float dropScoreRate = 15.0f;


        void Start()
        {
            // Set current static reference
            current = this;

            // Set Timer
            eggTimer = new Timer(eggTimerValue);
            eggTimer.OnComplete += SpawnEgg;
            stunTimer = new Timer(1f);
            stunTimer.OnComplete += EndStun;

            // Set Score
            currentPickScore = maxScore;

            // Auto-Get from the same GameObject
            rb2D = GetComponent<Rigidbody2D>();
            animControl = GetComponent<AnimationControl2D>();

        }
        void Update()
        {

            // End Update If not In Update
            if (ChickenLevelManager.instance.currentState != GAME_STATE.UPDATE) return;


            //Update Timers
            if (!stuned) eggTimer.Update();
            if (stuned)  stunTimer.Update();

            // Play Stunned Animation
            if (stuned) animControl.ChangeState("Stun");

            // Return if stunned
            if (stuned) return;

            // Drop Score
            if (minScore < currentPickScore) UpdatePickScore(); 

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


        // Update Score
        void UpdatePickScore()
        {
            float newScore = currentPickScore - (dropScoreRate * Time.deltaTime);
            currentPickScore = Mathf.Clamp(newScore, minScore, maxScore);
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

            // Update Animation 
            if (runDirection == Vector2.up) animControl.ChangeState("WalkUp");
            else if (runDirection == Vector2.down) animControl.ChangeState("WalkDown");
            else if (runDirection == Vector2.left) animControl.ChangeState("WalkLeft");
            else if (runDirection == Vector2.right) animControl.ChangeState("WalkRight");
            else if (runDirection == Vector2.zero) animControl.ChangeState("Idle");
        }

        /**
        *   Signals
        **/

        // Pick
        public void PickUp()
        {
            animControl.ChangeState("Idle");

            SoundController.Instance.SetSfx(pickSFX);
            ScoreManager.instance.AddScore((int)currentPickScore);

            // DEBUG
            //print( "Plus:"+ currentPickScore.ToString());
            //print("Total:" + ScoreManager.instance.GetScore().ToString());
        }

        // Spawn Egg and Reset Timer
        void SpawnEgg()
        {
            if (eggPrefab)
            {
                if (eggFolder) GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero), eggFolder);
                else GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero));
                if (eggSFX) SoundController.Instance.SetSfx(eggSFX);
            }

            eggTimer.Reset();
        }
        public void SetStunTime(float stunTime )
        {
            stuned = true;
            stunTimer = new Timer(stunTime);
            stunTimer.OnComplete += EndStun;
        }
        // End Stunned Condition
        void EndStun()
        {
            stuned = false;
            stunTimer.Reset();
        }
    }
}
