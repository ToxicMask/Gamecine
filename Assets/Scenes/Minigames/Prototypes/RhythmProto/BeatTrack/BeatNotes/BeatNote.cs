using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Controller;


namespace RythumProto.BeatTrack
{
    [System.Serializable]
    public enum NoteType { LEFT, DOWN, UP, RIGHT}


    public class BeatNote : MonoBehaviour
    {

        public NoteType noteType;
        float speed = 1.6f;
        float fallLimit = -3;



        // Update is called once per frame
        void Update()
        {
            transform.position = transform.position + (Vector3) (Time.deltaTime * speed * Vector2.down);

            if (transform.position.y < fallLimit) Destroy(gameObject);
        }



        // Retun false if not hit, retun true if hit
        public bool CheckHit(InputRow currentInput)
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
