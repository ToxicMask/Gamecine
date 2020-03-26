using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Sidescroller.Control { 

    public class Player_Fighter : Fighter
    {

        //  Function to set input each frame : Player 1 controller
        protected override void Update()
        {
            // Horizontal Movement
            movement_dir.x = Input.GetAxis("Horizontal");

            // Attack
            attack_act = Input.GetButtonDown("Action Primary");

        }


        protected override void LateUpdate()
        {
            base.LateUpdate();

            if (attack_act)
            {
                anim_control.SetTrigger("Attack");
            }
        }
    }
}
