using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;
using DuelProto.Scenary;
namespace Duel.Manager{
    public class DuelManager : MonoBehaviour
    {
        private static DuelManager instance;
        public static DuelManager Instance{
            get{
                if(instance == null){
                    instance = FindObjectOfType<DuelManager>();
                }
                return instance;
            }
        }
        public float matchTime;
        public float elapsedTime;
        public float waitTime = 5f;
        [SerializeField] Vector2 firstPlayerPos, secondPlayerPos;
        [SerializeField] Vector2[] wallPos;
        [SerializeField] Vector2[] firstPlayerAmmoPos, secondPlayerAmmoPos;
        [SerializeField] GameObject firstPlayer, secondPlayer, wall, ammo;
        [Header("Parents")]
        [SerializeField] Transform players;
        [SerializeField] Transform walls;
        [Header("Stats")]
        public int turnosMax;
        public int turnosJogado;
        public int firstPlayerScore, secondPlayerScore;
        private Timer timer;
        private void Start() {
            timer = new Timer(matchTime);
            GameTurnBegin();
        }
        private void Update() {
            timer.Update();
            timer.OnComplete += ResetGame;
            elapsedTime = timer.elapsed;
        }
        public void GameTurnBegin(){
            StartCoroutine(WaitForBegin());
            var _firstPlayer = Instantiate(firstPlayer, firstPlayerPos, Quaternion.identity, players);
            _firstPlayer.GetComponent<DuelistPlayer>().playerNumber = 1;
            DuelHud.Instance.antonio = _firstPlayer.GetComponent<DuelistPlayer>();
            var _secondPlayer = Instantiate(secondPlayer, secondPlayerPos, Quaternion.identity, players);
            _secondPlayer.GetComponent<DuelistPlayer>().playerNumber = 2;
            DuelHud.Instance.corisco = _secondPlayer.GetComponent<DuelistPlayer>();
            foreach(var t in wallPos){
                Instantiate(wall, t, Quaternion.identity, walls);
            }
        }
        IEnumerator WaitForBegin(){
            StateController.Instance.ChangeState(States.GAME_WAIT);
            yield return new WaitForSeconds(waitTime);
            StateController.Instance.ChangeState(States.GAME_UPDATE);
        }
        public void ResetGame(){
            var firstPlayer = FindObjectsOfType<DuelistPlayer>();
            foreach(var p in firstPlayer){
                Destroy(p.gameObject);
            }
            var walls = FindObjectsOfType<Wall>();
            foreach(var w in walls){
                Destroy(w.gameObject);
            }
            turnosJogado++;
            CheckForEndGame();
            timer.elapsed = 0;
            GameTurnBegin();
        }
        void CheckForEndGame(){
            if(turnosJogado >= turnosMax){
                turnosJogado = 0;
                firstPlayerScore = 0;
                secondPlayerScore = 0;
            }
        }
    }
}
