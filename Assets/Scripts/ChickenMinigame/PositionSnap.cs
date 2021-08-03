using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChickenGameplay.Navigate
{
    public class PositionSnap : MonoBehaviour
    {
        public static float snapFactor = .125f; // Adjust position to grid


        /*
         * Snap position acording to snapFactor
         */
        public void Snap()
        {
            // Validate Transform Position
            Vector3 tp = transform.position;

            tp = new Vector3
                (
                Mathf.Round(tp.x / snapFactor) * snapFactor ,
                Mathf.Round(tp.y / snapFactor) * snapFactor ,
                0
                );

            transform.position = tp;
        }

        private void OnValidate()
        {
           Snap();
        }
    }


}
