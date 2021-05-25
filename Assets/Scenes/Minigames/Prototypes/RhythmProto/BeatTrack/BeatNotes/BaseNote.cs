using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Controller;


/// <summary>
/// Store Base Note - Basic Behaviour for every type of Note
/// </summary>


namespace RythumProto.BeatTrack
{
    [System.Serializable]
    public enum NoteType { LEFT, DOWN, UP, RIGHT}
    [System.Serializable]
    public enum TrickType { A, B }


    public abstract class BaseNote : MonoBehaviour
    {
        protected float speed = 1.6f;
        protected float fallLimit = -3;


        protected virtual void Update()
        {
            transform.position = transform.position + (Vector3)(Time.deltaTime * speed * Vector2.down);

            if (transform.position.y < fallLimit) Destroy(gameObject);
        }



        // Retun false if not hit, retun true if hit
        public virtual bool CheckHit(InputRow currentInput)
        {
            return false;
        }
    }
}
