using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;
using UnityEngine.UI;
namespace Duel.Manager{
    public class DuelHud : MonoBehaviour
    {
        private static DuelHud instance;
        public static DuelHud Instance{
            get{
                if(instance == null){
                    instance = FindObjectOfType<DuelHud>();
                }
                return instance;
            }
        }
        public DuelistPlayer corisco, antonio;
        public Image[] coriscoAmmo, antonioAmmo;
        public Text timer;
        public Text coriscoScore, antonioScore;
        public Text turnos;
        private void Update() {
            int _timer = (int)DuelManager.Instance.matchTime - (int)DuelManager.Instance.elapsedTime;
            timer.text = _timer.ToString();
            CoriscoAmmo();
            AntonioAmmo();
            coriscoScore.text = DuelManager.Instance.secondPlayerScore.ToString();
            antonioScore.text = DuelManager.Instance.firstPlayerScore.ToString();
            turnos.text = $"{DuelManager.Instance.turnosJogado + "/" + DuelManager.Instance.turnosMax}";
        }
        void CoriscoAmmo(){
            for(int i = 0; i < coriscoAmmo.Length; i++){
                coriscoAmmo[i].enabled = false;
            }
            for(int i = 0; i < corisco.mainGun.bullets; i++){
                coriscoAmmo[i].enabled = true;
            }
        }
        void AntonioAmmo(){
            for(int i = 0; i < antonioAmmo.Length; i++){
                antonioAmmo[i].enabled = false;
            }
            for(int i = 0; i < antonio.mainGun.bullets; i++){
                antonioAmmo[i].enabled = true;
            }
        }
    }
}
