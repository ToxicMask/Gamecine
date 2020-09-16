using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MainMenu.Selection
{
    public class SelectionScreen : MonoBehaviour
    {

        static SelectionScreen current;

        public Transform inFrame;
        public Transform outOfFrame;

        public CanvasGroup buttonCanvas;
        

        private int currentMinigameID =1;

        bool canSelect = false;

        private void Awake()
        {
            current = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            // Set Out of frame
            current.gameObject.transform.position = outOfFrame.position;

            // Set as Disabled
            current.gameObject.SetActive(false);
        }

        private void Update()
        {
            if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Action Primary")) && canSelect) { StartMinigame(); }
        }

        public void ScrolScreenIn()
        {
            LeanTween.move(current.gameObject, inFrame.position, 4.5f).setOnComplete(ToogleSelectability);

        }
        

        public void ToogleSelectability()
        {
            canSelect = !canSelect;
        }

        public void StartMinigame()
        {
            Core.ChangeScene.MainMenu.StartMinigame(current.currentMinigameID);
        }
        public void StartMinigame(int id)
        {
            LeanTween.pauseAll();
            Core.ChangeScene.MainMenu.StartMinigame(id);
            
        }
    }
}
