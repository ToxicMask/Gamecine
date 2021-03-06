﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GuideCharacter
{
    public class FinalScoreDisplay : MonoBehaviour
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

            string template = "SCORE: {0}";

            string newCount = LevelManager.current.score.ToString();

            string newText = string.Format(template, newCount);

            displayCount.text = newText;
        }
    }
}
