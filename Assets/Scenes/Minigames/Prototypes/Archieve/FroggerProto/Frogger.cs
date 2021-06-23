using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.Frogger
{
    public class Frogger : MonoBehaviour
    {

        // Component
        Rigidbody2D rb;

        //Variable
        float step = .25f;
        Vector3 gridSize = Vector3.one;

        // Start is called before the first frame update
        void Awake()
        {
            // Auto Get
            rb = this.GetComponent<Rigidbody2D>();

            // Set Grid size
            SetGridSize(step);

            //Set Grid Size

        }

        // Update is called once per frame
        void Update()
        {
            // Game End
            if (FroggerGameManager.gameEnd) return;

            //Movement
            if (Input.anyKeyDown)
            {
                // Input Variables



                int vInput = (int)Input.GetAxis("Vertical");           // Vertical Input
                int hInput = (int)Input.GetAxis("Horizontal");         // Horizontal Input
                bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
                bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed

                if (vInput != 0)
                {

                    GridMovement(0, vInput);
                }

                else if (hInput != 0)
                {
                    GridMovement(hInput, 0);
                }
            }
        }

        // Kills if Not at the top of Anything
        private void LateUpdate()
        {
            // Game End
            if (FroggerGameManager.gameEnd) return;

            if (transform.parent == null)
            {
                Destroy(gameObject);
            }
        }

        void SetGridSize(float size)
        {
            gridSize = Vector3.one * size;
        }

        void GridMovement(int x, int y)
        {
            //Debug print("x -" + x.ToString() + "  " + "y -" + y.ToString());

            // Variables
            Vector2 move = new Vector2(x, y);
            Vector2 currentPosition = transform.position;
            Vector2 newPosition = currentPosition + (move * step);

            //Movement
            rb.transform.position = newPosition;

            //print(newPosition);
            //Snap Position
            //SnapToGrid(gridSize);
        }


        void SnapToGrid(Vector3 gridSize)
        {
            var currentPos = transform.position;
            transform.position = new Vector3(Mathf.Round(currentPos.x / gridSize.x),
                                             Mathf.Round(currentPos.y / gridSize.y),
                                             Mathf.Round(currentPos.z / gridSize.z));
        }
    }
}