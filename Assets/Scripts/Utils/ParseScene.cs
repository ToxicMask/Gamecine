using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseScene : MonoBehaviour
{
    [SerializeField] bool changeOnKey = false;
    public AllScenes sceneToChange;
    public void ChangeScene(AllScenes scene){
        Core.ChangeScene.MainMenu.LoadByName(scene.ToString());
    }
    private void Update() {
        if(!changeOnKey) return;
        if(Input.anyKeyDown){
            ChangeScene(sceneToChange);
        }
    }
}
