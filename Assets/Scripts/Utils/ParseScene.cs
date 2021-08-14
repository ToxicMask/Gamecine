using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseScene : MonoBehaviour
{
    [SerializeField] bool changeOnKey = false;
    public int id;
    public void ChangeScene(int id){
        Core.ChangeScene.MainMenu.StartMinigame(id);
    }
    private void Update() {
        if(!changeOnKey) return;
        if(Input.anyKeyDown){
            ChangeScene(id);
        }
    }
}
