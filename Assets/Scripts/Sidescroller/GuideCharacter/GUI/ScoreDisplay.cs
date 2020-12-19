using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter
{
    public class ScoreDisplay : MonoBehaviour
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

            string template = "SCORE: {0}";

            string newCount = LevelManager.current.score.ToString("D4");

            string newText = string.Format(template, newCount);

            displayCount.text = newText;
        }
    }
}
