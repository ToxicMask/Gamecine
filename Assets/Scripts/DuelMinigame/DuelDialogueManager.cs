using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DuelDialogueManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject textObject = null;
    [SerializeField] GameObject imagePortrait = null;
    [SerializeField] Text textDisplay = null;
    [Header("Parameters")]
    [SerializeField] DuelDialogue[] dialogues = null;
    [SerializeField] AudioClip music = null;
    [Header("Misc")]
    [SerializeField] AudioSource musicSource = null;
    [SerializeField] AudioSource soundSource = null;
    Dictionary<int, string[]> dialoguesList;
    Coroutine moving;
    ParseScene sceneParse;
    private void Start() {
        sceneParse = GetComponent<ParseScene>();
        ChangePositions(true);
        BuildDialogues();
        Invoke("StartDialogue", 1f);
        if(music != null) musicSource.clip = music;
        musicSource.Play();
    }
    public void ChangePositions(bool value){
        if(value)if(moving == null)moving = StartCoroutine(ShowAnimation());
        else{
            if(moving == null)moving = StartCoroutine(HideAnimation());
        }
    }
    void BuildDialogues(){
        dialoguesList = new Dictionary<int, string[]>();
        for(int i = 0; i < dialogues.Length; i++){
            dialoguesList.Add(i, dialogues[i].lines);
            Debug.Log($"Added {dialogues[i].lines.Length} dialogues to the list");
        }
    }
    void StartDialogue(){
        int startIndex = 0;
        int dialogueLine = 0;
        StartCoroutine(Type(startIndex, dialogueLine));
    }
    IEnumerator Type(int index, int line){
        foreach(var c in dialogues[index].lines[line]){
            if(dialogues[index].portrait != null){
                imagePortrait.GetComponent<Image>().enabled = true;
                imagePortrait.GetComponent<Image>().sprite = dialogues[index].portrait;
            }else{
                imagePortrait.GetComponent<Image>().enabled = false;
            }
            textDisplay.text += c;
            soundSource.clip = dialogues[index].talkingSound;
            soundSource.Play();
            yield return new WaitForSeconds(dialogues[index].talkingTime);
        }
        yield return new WaitForSeconds(1f);
        if(GetNextLine(index, line) == 99){
            Debug.Log("Fim de dialogo");
            yield return new WaitForSeconds(.25f);
            sceneParse.ChangeScene(sceneParse.sceneToChange);
        }else{
            textDisplay.text = " ";
            StartCoroutine(Type(index, GetNextLine(index, line)));
        }
    }
    int GetNextLine(int index, int line){
        var nextLine = line + 1;
        if(dialogues[index].lines.Length <= nextLine) return 99;
        else return nextLine;
    }
    IEnumerator ShowAnimation(){
        textObject.LeanMoveY(80f, .5f);
        imagePortrait.LeanMoveX(200f, .5f);
        yield return new WaitForSeconds(.5f);
        moving = null;
    }
    IEnumerator HideAnimation(){
        textObject.LeanMoveY(-80f, .5f);
        imagePortrait.LeanMoveX(-200f, .5f);
        yield return new WaitForSeconds(.5f);
        moving = null;
    }
}
