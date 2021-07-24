using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Navigate;


namespace ChickenGameplay.Chicken
{
    public class RunnerChicken : MonoBehaviour
    {
        //float snapFactor = .125f; // Adjust position to grid

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

        // Timer Values
        public float timerValue = 10f;
        public float timerLeft = 10f;


        // Stun
        public bool stuned = false;


        // Start is called before the first frame update
        void Start()
        {

            // Set current static reference
            current = this;


            // Set Timer
            timerLeft = timerValue;

            // Auto-Get from the same GameObject
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void Update()
        {



            //Update Timer
            timerLeft -= Time.deltaTime;

            if (timerLeft < 0)
            {


                if (eggPrefab)
                {
                    if (eggFolder) GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero), eggFolder);
                    else GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero));
                }
                stuned = false;
                timerLeft = timerValue;
            }

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

        // Check New Direction Chenge to horizontal to vertical and vice-versa
        private void OnTriggerStay2D(Collider2D collision)
        { 

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



        Vector2 GetRandomDirection(Vector2 excepition)
        {
            Vector2[] allDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.right, Vector2.left };

            int newIndex = Random.Range(1, 1000000) % 4;
            
            // Go to Next if it is exception
            if ( excepition == allDirections[newIndex]) newIndex = (newIndex+1)%4;

            return allDirections[newIndex];
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

        private void OnDestroy()
        {
            current = null;
        }
    }
}
