using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChickenGameplay.UI
{
    public class GameHUD : MonoBehaviour
    {
        public TMP_Text pointsText = null;

        public void UpdateScore(int newScore)
        {
            if (pointsText) pointsText.text = "POINTS\n" + newScore.ToString();
        }
    }
}
