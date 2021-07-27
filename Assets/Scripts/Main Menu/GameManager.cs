using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainMenu.Title;
using MainMenu.Selection;

namespace MainMenu.GameManager
{
    public enum MainMenuStates
    {
        TitleScreen,
        MinigameSelection,
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager current;

        public static MainMenuStates currentState = MainMenuStates.TitleScreen;

        public TitleScreen titleScreen;
        public SelectionScreen selectionScreen;

        private void Awake()
        {
            current = this;
        }

        private void Start()
        {
            currentState = MainMenuStates.TitleScreen;

            // Execute Title Screen
            ChangeStateAndExecute(currentState);
            
        }

        public void ChangeStateAndExecute(MainMenuStates newState)
        {
            currentState = newState;

            switch (currentState)
            {
                case MainMenuStates.TitleScreen:
                    DisplayTitleScreen();
                    break;

                case MainMenuStates.MinigameSelection:
                    DisplaySelectionScreen();
                    break;
            }

                
        }

        void DisplayTitleScreen()
        {
            titleScreen.gameObject.SetActive(true);
            titleScreen.SimpleDisplay();
        }

        void DisplaySelectionScreen()
        {
            Invoke("Display", 2.5f);
        }
        void Display(){
            selectionScreen.gameObject.SetActive(true);
        }
    }
}

