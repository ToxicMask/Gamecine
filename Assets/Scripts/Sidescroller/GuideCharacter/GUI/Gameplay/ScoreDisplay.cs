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

            string template = "SCORE: {0}";
            int fixedSize = 28;

            string newCount = LevelManager.current.score.ToString();

            string newText = string.Format(template, newCount);

            if (newText.Length < fixedSize)
            {
                newText = newText.Replace(" ", (new string(' ', (fixedSize - newText.Length))));
            }

            displayCount.text = newText;
        }
    }
}
