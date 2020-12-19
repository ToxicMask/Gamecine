using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter
{
    public class PaulistaGoalDisplay : MonoBehaviour
    {
        TMP_Text displayCount = null;

        private void Start()
        {
            // Auto Get
            displayCount = GetComponent<TMP_Text>();
        }

        private void LateUpdate()
        {

            UpdateDisplay();

        }

        private void UpdateDisplay()
        {

            string template = "GOAL: {0} / {1} (Min: {2})";

            string newCount = AutomaticCharacter.GetCurrentCount().ToString("D2");

            string newText = string.Format(template, LevelManager.current.paulistaOut, LevelManager.current.paulistasIn, LevelManager.current.minOut );

            displayCount.text = newText;
        }

    }
}
