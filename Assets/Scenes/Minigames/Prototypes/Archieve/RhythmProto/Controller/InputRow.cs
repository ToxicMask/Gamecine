using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// 
/// Structure to Process and Store Input
///



namespace RythumProto.Controller
{

    public class InputRow
    {
        public InputRow() { }


        // Set Up Class to Monitor
        public InputNote noteLeft = new InputNote();
        public InputNote noteDown = new InputNote();
        public InputNote noteUp = new InputNote();
        public InputNote noteRight = new InputNote();

        public InputNote noteA = new InputNote();
        public InputNote noteB = new InputNote();


        // Process Vertical and Horizontal Input
        public void ProcessInput(float hInput, float vInput, bool buttonA, bool buttonB)
        {
            // Minimum Value to counto to joystick
            float minAnalog = .5f;


            // Vertical

            // Down
            if (vInput <= -minAnalog) noteDown.Press();
            else noteDown.Release();

            // Up
            if (minAnalog <= vInput) noteUp.Press();
            else noteUp.Release();


            // Horizontal

            // Left
            if (hInput <= -minAnalog) noteLeft.Press();
            else noteLeft.Release();

            // Right
            if (minAnalog <= hInput) noteRight.Press();
            else noteRight.Release();


            //Special Buttons
            if (buttonA) noteA.Press();
            else noteA.Release();

            if (buttonB) noteB.Press();
            else noteB.Release();
        }
    }
}
