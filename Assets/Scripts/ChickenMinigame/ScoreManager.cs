using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.UI;

namespace ChickenGameplay.Score
{
    public class ScoreManager : MonoBehaviour
    {

        // Static Singletone
        public static ScoreManager instance = null;

        [Header("Player Score")]
        [SerializeField] int playerScore = 0;

        [Header("UI")]
        public GameHUD gameHUD = null;

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null;
        }

        public void AddScore(int addValue)
        {
            playerScore += addValue;
            gameHUD.UpdateScore(playerScore);
        }

        public void SetScore(int newValue)
        {
            playerScore = newValue;
            gameHUD.UpdateScore(playerScore);
        }
    }
}
