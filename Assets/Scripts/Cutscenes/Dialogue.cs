using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialogue Object")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] lines;
}

[System.Serializable]
public class DialogueLine
{
    public string character;

    [TextArea(3,8)]
    public string content;
}
