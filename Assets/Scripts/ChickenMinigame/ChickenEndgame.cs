using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChickenEndgame : MonoBehaviour
{
    [SerializeField]Text points = null;
    public static int playerPoints;
    private void Start() {
        points.text = $"{"Points : " + playerPoints}";
    }
}
