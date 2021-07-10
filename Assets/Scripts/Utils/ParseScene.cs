using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParseScene : MonoBehaviour
{
    public void ChangeScene(int id){
        Core.ChangeScene.MainMenu.StartMinigame(id);
    }
}
