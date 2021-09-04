using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseScene : MonoBehaviour
{
    [SerializeField] bool changeOnKey = false, changeOnStart = false;
    [SerializeField] float timer;
    public AllScenes sceneToChange;
    private void Start() {
        if(changeOnStart)StartCoroutine(ChangeScene());
    }
    public void ChangeScene(AllScenes scene){
        Core.ChangeScene.MainMenu.LoadByName(scene.ToString());
    }
    private void Update() {
        if(!changeOnKey) return;
        if(Input.anyKeyDown){
            ChangeScene(sceneToChange);
        }
    }
    IEnumerator ChangeScene(){
        yield return new WaitForSeconds(timer);
        ChangeScene(sceneToChange);
    }
}
