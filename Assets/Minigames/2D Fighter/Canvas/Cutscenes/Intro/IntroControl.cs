using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.StateMachine;
using TMPro;

namespace Sidescroller.Canvas
{

    public class IntroControl : MonoBehaviour
    {

        [SerializeField] Animator introAnimator = null;
        [SerializeField] Camera introCamera = null;

        //Dialogue Componets
        [SerializeField] TextMeshProUGUI charText = null;
        [SerializeField] TextMeshProUGUI lineText = null;


        //Dialogue Variables
        public Dialogue dialogAsset = null;
        private Queue<DialogueLine> dialogue = null;
        private bool dialogStarted = false;



        private void Awake()
        {
            //Fill Content
            dialogue = new Queue<DialogueLine>();

            if (dialogAsset)
            {
                foreach (DialogueLine line in dialogAsset.lines)
                {
                    dialogue.Enqueue(line);
                }
            }
        }


        private void Update()
        {
            // Next Line
            if (Input.GetButtonDown("Submit")) NextStep();

            //Skip Intro
            else if (Input.GetButtonDown("Cancel") && dialogStarted) EndIntro();
        }


        #region Text Methods

        private void NextStep()
        {
            // Start Dialog
            if (!dialogStarted) StartIntoText();

            // Continue Dialogue
            else NextLine();
        }

        private void StartIntoText()
        {
            dialogStarted = true;
            introAnimator.Play("TextDialog");

            NextLine();
        }

        private void NextLine()
        {
            // End Dialogue
            if (dialogue.Count <= 0)
            {
                EndIntro();
                return;
            }

            DialogueLine line = dialogue.Dequeue();

            string lineChar = line.character;
            string lineContent = line.content;

            //Alligment
            TextAlligment(lineChar);


            // Content
            charText.text = lineChar;
            lineText.text = lineContent;

            //Debug print(lineChar + ":" +lineContent);
        }

        private void TextAlligment(string charName)
        {
            //Left Indetment for Augusto
            if (charName[0] == 'a' || charName[0] == 'A')
            {
                charText.alignment = TextAlignmentOptions.BaselineLeft;
                lineText.alignment = TextAlignmentOptions.TopLeft;
            }
            //Left Indetment for Augusto
            else if (charName[0] == 'j' || charName[0] == 'J')
            {
                charText.alignment = TextAlignmentOptions.BaselineRight;
                lineText.alignment = TextAlignmentOptions.TopRight;
            }
            //Left Indetment for Augusto
            else
            {
                charText.alignment = TextAlignmentOptions.BaselineLeft;
                lineText.alignment = TextAlignmentOptions.TopLeft;
            }
        }

        #endregion


        #region Action Methods

        public void PlayIntro()
        {
            if (!PublicVariablesAvailable())
            {
                Debug.LogAssertion(" Reference missing! ");
                return;
            }

            // Config Animator
            introAnimator.SetBool("Playing", true);

            // Config Camera
            introCamera.enabled = true;
        }

        public void StopIntro()
        {
            if (!PublicVariablesAvailable()) return;

            introAnimator.SetBool("Playing", false);
            
            introCamera.enabled = false;
        }

        private void EndIntro()
        {
            Fighter2DMinigameStateMachine.current.StartSelectionSequence();
        }

        bool PublicVariablesAvailable()
        {
            if (introAnimator == null) return false;
            if (introCamera == null) return false;

            return true;
        }

        #endregion
    }
}
