using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenPrototype.Scenary;
using ChickenPrototype.Navigate;


namespace ChickenPrototype.Chicken
{
    public class Chicken : MonoBehaviour
    {
        //float snapFactor = .125f; // Adjust position to grid

        // Components
        [SerializeField] Rigidbody2D rb2D;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;
        // Direction Control Variable
        bool isReadyToChangeDirection = false;

        //Movement Values
        float runSpeed = 1.2f;
        float hitWallReach = .15f;

        // Timer Values
        public GameObject eggPrefab;

        public float timerValue = 10f;
        float timerLeft = 10f;


        // Start is called before the first frame update
        void Start()
        {
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
                if (eggPrefab) GameObject.Instantiate(eggPrefab, transform.position, Quaternion.Euler(Vector3.zero));
                timerLeft = timerValue;
                
            }

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
            if (!isReadyToChangeDirection) return;


            DirectionGuide dg = collision.GetComponent<DirectionGuide>();
            float distance = (transform.position - collision.transform.position).magnitude;
            float threshold = 0.02f;

            if (dg && (distance < threshold))
            {
                // Eliminate Theshhold
                transform.position = collision.transform.position;

                // Eliminate Repetition
                isReadyToChangeDirection = false;

                // Set New Direction
                Vector2 newDirection = dg.GetSetDirection(runDirection);

                ChangeRunDirection(newDirection);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            isReadyToChangeDirection = true;
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
    }
}





/// Trash Code

/**
private void OnCollisionStay2D(Collision2D collision)
{
    print(collision.collider + "@");

    Vector2 contactNormal = collision.GetContact(0).normal;

    // If contact is oposite of Run Direction -> Set new Direction
    if (contactNormal == -runDirection )
    {
        ChangeRunDirection(GetRandomDirection(runDirection));
        print("BOUNCE!" + runDirection.ToString());
    }
}
**/
