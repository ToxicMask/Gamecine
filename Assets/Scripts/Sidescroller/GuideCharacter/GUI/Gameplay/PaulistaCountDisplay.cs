using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter
{
    public class PaulistaCountDisplay : MonoBehaviour
    {

        TMP_Text displayCount = null;

        private void Awake()
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

            string template = "PAULISTAS: {0}";

            string newCount = AutomaticCharacter.GetCurrentCount().ToString("D2");

            string newText = string.Format(template, newCount);

            displayCount.text = newText;
        }
    }
}
