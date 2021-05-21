using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.BeatTrack;


/* Anotações
 *
 * Eixos são limitados pelo analógico --> Limitação 
 * 
 * 
 */


namespace RythumProto.Controller
{
    public class RythmController : MonoBehaviour
    {
        // Controller Output
        [SerializeField] SpriteRenderer spriteLeft = null;
        [SerializeField] SpriteRenderer spriteDown = null;
        [SerializeField] SpriteRenderer spriteUp = null;
        [SerializeField] SpriteRenderer spriteRight = null;

        // Scan Area
        [SerializeField] Vector2 noteAreaOffset = Vector2.zero;
        [SerializeField] Vector2 noteAreaSize = new Vector2(4.5f, .25f);
        public bool activeGizmo = true;

        // Input Classes
        InputRow inputRow = new InputRow();


        void Update()
        {

            // Get Input
            float hInput = Input.GetAxis("Horizontal");             // Horizontal Input
            float vInput = Input.GetAxis("Vertical");               // Vertical Input
            
            bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
            bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed



            // Set Input Process
            inputRow.ProcessInput(hInput, vInput);


            // Show in Interface
            if (spriteLeft) spriteLeft.enabled  = inputRow.noteLeft.GetIsPressed();
            if (spriteDown) spriteDown.enabled  = inputRow.noteDown.GetIsPressed();
            if (spriteUp)   spriteUp.enabled    = inputRow.noteUp.GetIsPressed();
            if (spriteRight) spriteRight.enabled= inputRow.noteRight.GetIsPressed();

            //End Loop if No Note Input
            if (hInput == 0 && vInput == 0) return;

            // Get Nodes
            Collider2D[] colliders = DetectColliders();

            // Process Node
            NoteProcess(colliders);
        }


        Collider2D[] DetectColliders()
        {
            Vector2 scanPosition = (Vector2) transform.position + noteAreaOffset;
            Vector2 scanSize = noteAreaSize;

            Vector2 pointA = scanPosition - (scanSize / 2);
            Vector2 pointB = scanPosition + (scanSize / 2);

            Collider2D[] colliders = Physics2D.OverlapAreaAll(pointA, pointB);


            return colliders;
        }

        void NoteProcess(Collider2D[] colliders)
        {
            foreach (Collider2D collider in colliders)
            {
                BeatNote bn = collider.GetComponent<BeatNote>();

                if (bn)
                {
                    // Check
                    bn.CheckHit(inputRow);
                }
            }
        }

        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Vector2 scanPosition = (Vector2) transform.position + noteAreaOffset;
            Vector2 scanSize = noteAreaSize;

            Gizmos.DrawWireCube(scanPosition, scanSize);
        }
        
    }
}
