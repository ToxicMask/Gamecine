using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChickenPrototype.Chicken
{
    public class Chicken : MonoBehaviour
    {

        readonly float snapFactor = .125f; // Adjust position to grid

        // Components
        [SerializeField] Rigidbody2D rb2D;

        // Movement Variables -> Auto Walk
        Vector2 runDirection = Vector2.right;
        [SerializeField] float runSpeed = 1.2f;


        // Start is called before the first frame update
        void Start()
        {
            // Auto-Get from the same GameObject
            if (!rb2D) rb2D = GetComponent<Rigidbody2D>();

        }

        // Update is called once per frame
        void Update()
        {

            // Set Input Variables
            float vInput = 0;             // Vertical Input
            float hInput = 0;           // Horizontal Input


            //Process Input
            if (vInput != 0 || hInput != 0) ChangeRunDirection(vInput, hInput);


            // Execute Movement
            RunMovement();

        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            print(collision.collider + "@");

            Vector2 contactNormal = collision.GetContact(0).normal;

            // If contact is oposite of Run Direction -> Set new Direction
            if (contactNormal == -runDirection )
            {
                runDirection = GetRandomDirection(runDirection);
                print("BOUNCE!" + runDirection.ToString());
            }
        }


        Vector2 GetRandomDirection( Vector2 excepition)
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
        private void ChangeRunDirection(float vInput, float hInput)
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
