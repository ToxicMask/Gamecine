using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter
{
    public class TotalPaulistaDisplay : MonoBehaviour
    {

        TMP_Text displayCount = null;

        private void Awake()
        {
            // Auto Get
            displayCount = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {

            UpdateDisplay();

        }


        private void UpdateDisplay()
        {

            string template = "TOTAL: {0} / {1}";

            string newText = string.Format(template, LevelManager.current.paulistaOut, LevelManager.current.paulistasIn);

            displayCount.text = newText;
        }
    }
}
