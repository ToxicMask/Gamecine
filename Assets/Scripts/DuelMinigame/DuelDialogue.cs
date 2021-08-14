using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Duel_Dialogue", menuName = "Duel/Dialogue", order = 1)]
public class DuelDialogue : ScriptableObject
{
    public Sprite portrait;
    public AudioClip talkingSound;
    public float talkingTime;
    public string[] lines;
}
