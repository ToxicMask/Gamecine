using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GuideCharacter
{
    public class LevelManager : MonoBehaviour
    {
        // Singletons
        public static LevelManager current = null;

        // Events
        public delegate void LevelEvent();
        public event LevelEvent OnLevelCompleted;

        // Expressions
        private bool InputKeyPress => Input.GetButtonDown("Action Primary") || Input.GetButtonDown("Action Secondary");

        // Level State

        [Tooltip("Number of Spawn Paulistas in the Level")]
        public int paulistasIn = 10;
        [Tooltip("Number of  Paulistas that fineshed the Level")]
        public int paulistaOut = 0;
        [Tooltip("Minimal number of OutPaulistas to Win the Level")]
        public int minOut = 5;

        public int score = 0;


        #region Unity Methods

        private void Awake()
        {
            current = this;

            score = 0;
        }

        private void OnDestroy()
        {
            if (LevelManager.current == this) current = null;
        }

        #endregion

        public void LevelEnd(bool isVictory)
        {
            if (isVictory) StartCoroutine("LevelCompleted");
        }


        private IEnumerator LevelCompleted()
        {
            // Perform Action event
            if (OnLevelCompleted != null) OnLevelCompleted();

            print("Victory!");
            

            yield return new WaitWhile(() => !InputKeyPress);
            

            SceneManager.LoadScene((int)AllScenes.MainMenu);

            yield return null;
        }
    }
}
