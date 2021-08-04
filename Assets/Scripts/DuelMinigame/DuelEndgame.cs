using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DuelEndgame : MonoBehaviour
{
    [SerializeField]Text winnerName = null;
    public static string winner;
    public static int winnerPoints;
    private void Start() {
        winnerName.text = $"{"Winner : " + winner + "\n" + "Points : " + winnerPoints}";
    }
    public void ChangeScene(int id){
        SceneManager.LoadScene(id);
    }
}
