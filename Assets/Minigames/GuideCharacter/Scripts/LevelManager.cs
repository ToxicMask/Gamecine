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


        #region Unity Methods

        private void Awake()
        {
            current = this;
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

            print("Total A. Character: " + AutomaticCharacter.totalCharacters.ToString());
            print("Victory!");
            

            yield return new WaitWhile(() => !InputKeyPress);

            print("Total A. Character: " + AutomaticCharacter.totalCharacters.ToString());

            SceneManager.LoadScene((int)AllScenes.MainMenu);

            yield return null;
        }
    }
}
