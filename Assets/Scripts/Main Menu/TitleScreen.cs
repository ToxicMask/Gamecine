using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MainMenu.Title
{
    public class TitleScreen : MonoBehaviour
    {

        static TitleScreen current;

        public CanvasGroup canvastButton;
        public AnimationCurve buttonCurve;
        public bool activeButton = true;

        public RectTransform inFrame;
        public RectTransform outFrame;

        public AudioClip tapeSound;

        private void Awake()
        {
            current = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            activeButton = true;
        }

        private void Update()
        {
            // Control Enter
            if (Input.anyKeyDown) current.StartButtonClick();
        }

        #region Button Methods

        public void StartButtonNormal() { }

        public void StartButtonClick() {
            if (!activeButton) return;

            activeButton = false;
            AudioSource.PlayClipAtPoint(tapeSound, Vector3.back * 10, 1f);
            LeanTween.alphaCanvas(canvastButton, 0, 2f).setEase(buttonCurve).setOnComplete(ChangeToSelectionScreen);
        }

        public void StartButtonDisable() {
            canvastButton.gameObject.SetActive(false);
        }

        #endregion



        #region Display Methods

        public void SimpleDisplay()
        {
            // Set Button Status
            current.StartButtonNormal();

            // Set position
            current.gameObject.transform.position = inFrame.transform.position;
        }

        public void ScrolScreenIn()
        {
            LeanTween.move(current.gameObject, inFrame, 1.5f);
        }

        public void ScrolScreenOut()
        {
            LeanTween.move(current.gameObject, outFrame, 2.5f).setOnComplete(HideScreen);
            StartButtonDisable();
        }

        public void HideScreen()
        {
            current.gameObject.SetActive(false);
        }


        private void ChangeToSelectionScreen()
        {
            ScrolScreenOut();
            GameManager.GameManager.current.ChangeStateAndExecute(GameManager.MainMenuStates.MinigameSelection);

        }
        #endregion
    }
}
