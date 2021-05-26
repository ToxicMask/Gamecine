using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RythumProto.Timer;

namespace RythumProto.BeatTrack
{
    public class SpawnNotes : MonoBehaviour
    {
        //Seamless Transition
        public Transform[] targetTracks = null;

        // Prefab
        public GameObject notePrefab;

        public void Spawn()
        {
            //Instanciate Node
            GameObject note;

            Transform targetTrack = null;

            // Get target Track
            if (targetTracks.Length != 0) targetTrack = targetTracks[Random.Range((int)0, (int)targetTracks.Length - 1)];

            if (targetTrack != null) note = GameObject.Instantiate(notePrefab, targetTrack);
            else                     note = GameObject.Instantiate(notePrefab, gameObject.transform);

            

            // Config Trick Note
            TrickNote tNote = note.GetComponent<TrickNote>();

            if (tNote && targetTrack)
            {
                // Same Height
                Vector2 targetPosition = ( transform.position - targetTrack.position) * Vector2.up;

                tNote.LeanMove(transform.position, targetPosition);
            }

        }
    }
}