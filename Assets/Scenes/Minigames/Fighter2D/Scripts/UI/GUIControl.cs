using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Sidescroller.Canvas
{
    public class GUIControl : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI fighter1ScoreDisplay = null;
        [SerializeField] TextMeshProUGUI fighter2ScoreDisplay = null;

        [SerializeField] TextMeshProUGUI roundDisplay = null;
        [SerializeField] TextMeshProUGUI finalRoundDisplay = null;


        public void UpdateRoundDisplay(int newRound, bool isFinalRound )
        {
            // Update Round Conter
            roundDisplay.SetText(newRound.ToString());

            // Display Final Mark; if is final round
            finalRoundDisplay.gameObject.SetActive(isFinalRound);
        }

        public void UpdateFightersScore(int f1Score, int f2Score)
        {
            fighter1ScoreDisplay.SetText(f1Score.ToString());
            fighter2ScoreDisplay.SetText(f2Score.ToString());
        }
    }
}
