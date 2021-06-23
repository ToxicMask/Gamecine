using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Controller;


/// <summary>
/// Trick Note - Note to disturb the flow
/// </summary>

namespace RythumProto.BeatTrack
{

    public class TrickNote : BaseNote
    {
        public TrickType trickType;

        public Vector2 target;


        public void LeanMove(Vector2 origin, Vector2 dest, float arriveTime = 0.5f)
        {

            target = dest;

            // Set origin of move
            transform.position = origin;

            LeanTween.moveLocal(gameObject, dest, arriveTime);
        }

        // Retun false if not hit, retun true if hit
        public override bool CheckHit(InputRow currentInput)
        {
            InputNote inputNote = null;

            switch (trickType)
            {
                case TrickType.A:
                    inputNote = currentInput.noteA;
                    break;

                case TrickType.B:
                    inputNote = currentInput.noteB;
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
