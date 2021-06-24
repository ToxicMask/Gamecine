using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;
namespace DuelProto.Manager{
    public class DuelProtoManager : MonoBehaviour
    {
        private static DuelProtoManager instance;
        public static DuelProtoManager Instance{
            get{
                if(instance == null){
                    instance = FindObjectOfType<DuelProtoManager>();
                }
                return instance;
            }
        }
        public float matchTime;
        public float elapsedTime;
        [SerializeField] Vector2 firstPlayerPos, secondPlayerPos;
        [SerializeField] Vector2[] wallPos;
        [SerializeField] Vector2[] firstPlayerAmmoPos, secondPlayerAmmoPos;
        [SerializeField] GameObject firstPlayer, secondPlayer, wall, ammo;
        [Header("Parents")]
        [SerializeField] Transform players;
        [SerializeField] Transform walls;
        [Header("Stats")]
        [SerializeField] int turnosMax;
        [SerializeField] int turnosJogado;
        public int firstPlayerScore, secondPlayerScore;
        private Timer timer;
        private void Start() {
            timer = new Timer(matchTime);
            GameTurnBegin();
        }
        private void Update() {
            timer.Update();
            timer.OnComplete += GameTurnBegin;
            elapsedTime = timer.elapsed;
        }
        public void GameTurnBegin(){
            var _firstPlayer = Instantiate(firstPlayer, firstPlayerPos, Quaternion.identity, players);
            _firstPlayer.GetComponent<DuelistPlayer>().playerNumber = 1;
            var _secondPlayer = Instantiate(secondPlayer, secondPlayerPos, Quaternion.identity, players);
            _secondPlayer.GetComponent<DuelistPlayer>().playerNumber = 2;
            foreach(var t in wallPos){
                Instantiate(wall, t, Quaternion.identity, walls);
            }
        }
    }
}
