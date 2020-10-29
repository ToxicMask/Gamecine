using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TopDownCar.Movement;
using TopDownCar.Animation;

namespace TopDownCar.Controller
{
    public class CarController : MonoBehaviour
    {
        bool Steering = false;

        [SerializeField] CarMovement carMovement;
        [SerializeField] CarAnimation carAnimation;

        // Start is called before the first frame update
        void Start()
        {
            // Auto Get
            if (carMovement == null) carMovement = GetComponent<CarMovement>();
            if (carAnimation == null) carAnimation = GetComponent<CarAnimation>();

        }

        // Update is called once per frame
        void Update()
        {

            // Process Player Input - Singleplayer
            ProcessInput();
            
        }


        void ProcessInput( )
        {


            // Controls //

            // Steer { Left / Right }

            // Left
            if (Input.GetAxis("Horizontal") < 0 && !Steering) {
                carMovement.TurnLeft();
                carAnimation.SetCarDirection(carMovement.direction);
                Steering = true;
                print("LEFT!");
            }

            // Right
            else if (Input.GetAxis("Horizontal") > 0 && !Steering) {
                carMovement.TurnRight();
                carAnimation.SetCarDirection(carMovement.direction);
                Steering = true;
                print("Right!");
            }

            else if ((Input.GetAxis("Horizontal") == 0)) { Steering = false; }

            // Accel / Break
            if (Input.GetButton("Action Primary" ))
            {
                carMovement.GoFoward();
            }

            if (Input.GetButton("Action Secondary"))
            {
                print("Go back!");
                carMovement.Break();
            }
        }
    }

}