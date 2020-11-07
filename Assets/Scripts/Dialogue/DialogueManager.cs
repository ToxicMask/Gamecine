using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    //Dialogue Componets
    [SerializeField] TextMeshProUGUI charText = null;
    [SerializeField] TextMeshProUGUI lineText = null;


    //Dialogue Variables
    public Dialogue dialogAsset = null;
    private Queue<DialogueLine> dialogue = null;
    public bool active = false;



    private void Awake()
    {
        // Active
        active = false;

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

    public void SetNewDialogue(Dialogue newDialogue)
    {
        dialogue.Clear();

        dialogAsset = newDialogue;

        if (dialogAsset)
        {
            foreach (DialogueLine line in dialogAsset.lines)
            {
                dialogue.Enqueue(line);
            }
        }
    }

    // Returns True When it is done
    public void NextStep()
        //Returns null if not active
    {
        // Start Dialogue to return
        if (!active) return;

        // Continue Dialogue
        else NextLine();
    }

    public void StartIntoText()
    {
        //Set as Active
        active = true;

        // Start Dialogue
        NextLine();
    }

    private void NextLine()
    {
        // End Dialogue
        if (dialogue.Count <= 0 )
        {
            active = false;
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
            charText.alignment = TextAlignmentOptions.BaselineGeoAligned;
            lineText.alignment = TextAlignmentOptions.Top;
        }
    }

}
