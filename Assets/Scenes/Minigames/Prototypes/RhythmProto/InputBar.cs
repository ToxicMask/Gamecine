using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Anotações
 *
 * Eixos são limitados pelo analógico --> Limitação 
 * 
 * 
 */


namespace RythumProto.Controller
{

    public class InputBar : MonoBehaviour
    {
        // Controller Output
        [SerializeField] SpriteRenderer spriteLeft = null;
        [SerializeField] SpriteRenderer spriteDown = null;
        [SerializeField] SpriteRenderer spriteUp = null;
        [SerializeField] SpriteRenderer spriteRight = null;

        // Input Classes
        InputTrack inputTrack = new InputTrack();



        void Update()
        {

            // Get Input
            float hInput = Input.GetAxis("Horizontal");             // Horizontal Input
            float vInput = Input.GetAxis("Vertical");               // Vertical Input
            
            bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
            bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed



            // Set Input Process
            inputTrack.ProcessInput(hInput, vInput);


            // Show in Interface
            if (spriteLeft) spriteLeft.enabled  = inputTrack.noteLeft.GetIsPressed();
            if (spriteDown) spriteDown.enabled  = inputTrack.noteDown.GetIsPressed();
            if (spriteUp)   spriteUp.enabled    = inputTrack.noteUp.GetIsPressed();
            if (spriteRight) spriteRight.enabled= inputTrack.noteRight.GetIsPressed();
        }
    }
}
