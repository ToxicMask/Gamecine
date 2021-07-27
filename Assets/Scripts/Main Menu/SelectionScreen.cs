using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace MainMenu.Selection
{
    public class SelectionScreen : MonoBehaviour
    {

        static SelectionScreen current;

        public Transform inFrame;
        public Transform outOfFrame;

        public CanvasGroup buttonCanvas;
        public MinigameInfo[] minigames;

        private int currentMinigameID = (int) AllScenes.AugustoMatraga;

        bool canSelect = true;
        MinigameInfo currentMinigame;
        int index = 0;
        bool canCommand = false;
        private void Awake()
        {
            current = this;
        }

        // Start is called before the first frame update
        void Start()
        {

            // Set as Disabled
            current.gameObject.SetActive(false);
            foreach(var m in minigames){
                m.onSelect += () => StartMinigame(m.sceneNumber);
                m.AddListener();
            }
            minigames[0].OnSelect();
            currentMinigame = minigames[0];
        }

        private void Update()
        {
            if(Input.GetAxis("Horizontal") != 0) KeyboardControl();
            else canCommand = true;
            if ((Input.GetButtonDown("Submit") || Input.GetButtonDown("Action Primary")) && canSelect){
                currentMinigame.Selected();
            }
        }
        

        public void ToogleSelectability()
        {
            canSelect = !canSelect;
        }
        void KeyboardControl(){
            if(Input.GetAxis("Horizontal") == -1){
                if(canCommand){
                    index++;
                    IndexNumber(index);
                    canCommand = false;
                }
            }
            if(Input.GetAxis("Horizontal") == 1){
                if(canCommand){
                    index--;
                    IndexNumber(index);
                    canCommand = false;
                }
            }
            currentMinigame.OnDeselect();
            minigames[index].OnSelect();
            currentMinigame = minigames[index];
        }
        int IndexNumber(int idx){
            if(idx > minigames.Length - 1){
                idx = 0;
            }
            if(idx < 0){
                idx = minigames.Length - 1;
            }
            index = idx;
            return idx;
        }
        /*public void StartMinigame()
        {
            Core.ChangeScene.MainMenu.StartMinigame(current.currentMinigameID);
        }*/
        public void StartMinigame(int id)
        {
            LeanTween.pauseAll();
            Core.ChangeScene.MainMenu.StartMinigame(id);
            
        }
    }
    [System.Serializable]
    public class MinigameInfo{
        public Image minigameImage;
        public Button minigameButton;
        public int sceneNumber;
        public event Action onSelect = delegate{};
        public MinigameInfo(Image image, Button button, int number){
            minigameImage = image;
            minigameButton = button;
            sceneNumber = number;
        }
        public void OnSelect(){
            minigameImage.gameObject.LeanScale(new Vector3(1.35f, 1.35f, 1.35f), .15f);
        }
        public void Selected(){
            onSelect();
        }
        public void AddListener(){
            minigameButton.onClick.AddListener(Selected);
        }
        public void OnDeselect(){
            minigameImage.gameObject.LeanScale(new Vector3(1f, 1f, 1f), .15f);
        }
    }
}
