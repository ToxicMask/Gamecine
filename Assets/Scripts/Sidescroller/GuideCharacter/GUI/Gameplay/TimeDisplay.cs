using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GuideCharacter;


public class TimeDisplay : MonoBehaviour
{

    TMP_Text displayCount = null;

    private void Awake()
    {
        // Auto Get
        displayCount = GetComponent<TMP_Text>();
    }

    private void LateUpdate()
    {

        UpdateDisplay();
    }


    private void UpdateDisplay()
    {

        int new_value = (int)Timer.current.TimeLeft;


        string template = "{1} : {0}";

        string newSeconds = (new_value % 60).ToString("D2");

        string newMinutes = ((new_value-(new_value % 60)) / 60).ToString("D2");


        string newText = string.Format(template, newSeconds, newMinutes);


        displayCount.text = newText;
    }
}
