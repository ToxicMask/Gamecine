using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCar.Animation
{
    public class CarAnimation : MonoBehaviour
    {

        [SerializeField] Animator animControl;

        // Start is called before the first frame update
        void Start()
        {
            // Auto get
            if (animControl == null) animControl = GetComponent<Animator>();

            // Set car Up
            SetCarDirection(Vector2.up);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetCarDirection(Vector2 newDirection)
        {
            animControl.SetFloat("moveX", newDirection.x);
            animControl.SetFloat("moveY", newDirection.y);
            //print("TURN");
        }
    }
}