using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Controller;


/// <summary>
/// BeatNote - Set to set the Correct Rythum of Input
/// </summary>

namespace RythumProto.BeatTrack
{
    public class BeatNote : BaseNote
    {

        public NoteType noteType;


        // Retun false if not hit, retun true if hit
        public override bool CheckHit(InputRow currentInput)
        {
            InputNote inputNote = null;

            switch (noteType)
            {
                case NoteType.LEFT:
                    inputNote = currentInput.noteLeft;
                    break;

                case NoteType.DOWN:
                    inputNote = currentInput.noteDown;
                    break;

                case NoteType.UP:
                    inputNote = currentInput.noteUp;
                    break;

                case NoteType.RIGHT:
                    inputNote = currentInput.noteRight;
                    break;
            }


            // Cancel if Null
            if (inputNote == null) return false;


            // Destroy Self if hit, just pressed
            if (inputNote.GetJustPressed())
            {
                Destroy(gameObject);
            }

            return true;
        }
    }
}
