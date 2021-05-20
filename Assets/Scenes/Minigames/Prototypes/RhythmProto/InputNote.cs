using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythumProto.Controller
{
    // Class to remember Input of each node
    class InputNote
    {
        public InputNote() { }


        private bool isPressed = false;
        int frameLastPressed = -1;

        // Return is Pressed
        public bool Press()
        {
            // If not already pressed then Press
            if (!isPressed)
            {
                isPressed = true;
                frameLastPressed = Time.frameCount;
            }

            return isPressed;
        }


        // Return Released isPressed
        public bool Release()
        {
            if (isPressed)
            {
                isPressed = false;
                return true;
            }

            return false;
        }


        public bool GetIsPressed()
        {
            return isPressed;
        }


        // Return true if Click is in the Same Frame
        public bool GetJustPressed()
        {
            return (isPressed && frameLastPressed == Time.frameCount);
        }
    }
}
