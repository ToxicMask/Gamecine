using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Control
{
    public abstract class Fighter : MonoBehaviour
    {




        #region Input Variables

        [SerializeField]
        protected Vector2 movement_dir = new Vector2();

        [SerializeField]
        protected bool attack_act = false;

        //defense_act = false;

        #endregion


        #region Components

        [SerializeField]
        protected Animator anim_control;

        [SerializeField]
        protected CharacterController char_control;

        #endregion


        #region Movement Parameters

        protected float walk_speed = .6f;

        #endregion



        // Get parameters
        protected virtual void Start()
        {
            // Auto Get Components

            anim_control = GetComponent<Animator>();

            char_control = GetComponent<CharacterController>();

        }

        //  Function to set input each frame
        protected virtual void Update()
        {


        }


        // Function to set Character Action each physics
        protected virtual void FixedUpdate()
        {
            Vector2 linear_velocity = movement_dir * walk_speed;

            char_control.Move(linear_velocity * Time.deltaTime);
        }


        // Function to update animator
        protected virtual void LateUpdate()
        {

        }

    }
}
