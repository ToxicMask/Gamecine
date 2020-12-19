using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter {
    public class LevelVictoryDisplay : MonoBehaviour
    {

        TMP_Text displayCount = null;

        private void Awake()
        {
            // Auto Get
            displayCount = GetComponent<TMP_Text>();
        }

        public void DisplayResult(bool isVictory)
        {
            string template = "{0}!";

            string result = "";

            if (isVictory)
            {
                result = "You  win";
            }
            else
            {
                result = "you  lose";
            }

            string newText = string.Format(template, result);

            displayCount.text = newText;

            print(newText);
        }
    }
}
