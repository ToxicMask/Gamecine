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
        [Header("Macth")]
        public float matchTime;
        public float elapsedTime;
        public float waitTime = 5f;
        [Header("Positions")]
        [SerializeField] Vector2 firstPlayerPos;
        [SerializeField] Vector2 secondPlayerPos;
        [SerializeField] Vector2[] wallPos;
        [SerializeField] Vector2[] npcPos;
        [Header("Prefabs")]
        [SerializeField] GameObject firstPlayer;
        [SerializeField] GameObject secondPlayer;
        [SerializeField] GameObject wall;
        [SerializeField] GameObject[] npcsObj;
        [Header("Parents")]
        [SerializeField] Transform players;
        [SerializeField] Transform walls;
        [SerializeField] Transform bullets;
        [SerializeField] Transform npcs;
        [Header("Audios")]
        [SerializeField] AudioClip inicioDeTurno, fimDeTurno, fimDaPartida;
        [Header("Stats")]
        public int turnosMax;
        public int turnosJogado;
        public int firstPlayerScore, secondPlayerScore;
        private Timer timer;
        private void Start() {
            timer = new Timer(matchTime);
            timer.OnComplete += ResetGame;
            GameTurnBegin();
        }
        private void Update() {
            timer.Update();
            elapsedTime = timer.elapsed;
        }
        public void GameTurnBegin(){
            StartCoroutine(WaitForBegin());
            SoundController.Instance.SetSfx(inicioDeTurno);
            var _firstPlayer = Instantiate(firstPlayer, firstPlayerPos, Quaternion.identity, players);
            _firstPlayer.GetComponent<DuelistPlayer>().playerNumber = 1;
            _firstPlayer.GetComponent<DuelistPlayer>().bulletFolder = bullets;
            DuelHud.Instance.antonio = _firstPlayer.GetComponent<DuelistPlayer>();
            var _secondPlayer = Instantiate(secondPlayer, secondPlayerPos, Quaternion.identity, players);
            _secondPlayer.GetComponent<DuelistPlayer>().playerNumber = 2;
            _secondPlayer.GetComponent<DuelistPlayer>().bulletFolder = bullets;
            DuelHud.Instance.corisco = _secondPlayer.GetComponent<DuelistPlayer>();
            foreach(var t in wallPos){
                Instantiate(wall, t, Quaternion.identity, walls);
            }
            Instantiate(npcsObj[0], npcPos[0], Quaternion.identity, npcs);
            Instantiate(npcsObj[1], npcPos[1], Quaternion.identity, npcs);
        }
        IEnumerator WaitForBegin(){
            StateController.Instance.ChangeState(States.GAME_WAIT);
            yield return new WaitForSeconds(waitTime);
            StateController.Instance.ChangeState(States.GAME_UPDATE);
        }

        public void ResetGame(){
            SoundController.Instance.SetSfx(fimDeTurno);
            var firstPlayer = FindObjectsOfType<DuelistPlayer>();
            foreach(var p in firstPlayer){
                Destroy(p.gameObject);
            }
            var walls = FindObjectsOfType<Wall>();
            foreach(var w in walls){
                Destroy(w.gameObject);
            }
            var npcs = FindObjectsOfType<Npc>();
            foreach(var n in npcs){
                Destroy(n.gameObject);
            }
            turnosJogado++;
            CheckForEndGame();
            timer.Reset();
            GameTurnBegin();
        }
        void CheckForEndGame(){
            if(turnosJogado >= turnosMax){
                turnosJogado = 0;
                firstPlayerScore = 0;
                secondPlayerScore = 0;
                SoundController.Instance.SetSfx(fimDaPartida);
            }
        }
    }
}
