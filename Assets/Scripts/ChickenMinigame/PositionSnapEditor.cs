using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace ChickenGameplay.Navigate
{
    
    [CustomEditor(typeof(PositionSnap))]
    public class PositionSnaptEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            PositionSnap pSnap = (PositionSnap)target;

            if (GUILayout.Button("SNAP"))
            {
                pSnap.Snap(); // Execute the Snap
            }
        }
    }
    
}

