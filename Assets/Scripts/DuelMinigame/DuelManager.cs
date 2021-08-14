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
        public float beginWaitTime = 5f;
        public float endWaitTime = 5f;
        public float endGameWaitTime = 5f;
        [Tooltip("If TRUE, match will end if one player reached minimal points for Victory.")]
        public bool scoreEndsMatch = true;
        [Header("Positions")]
        [SerializeField] Vector2 firstPlayerPos = Vector2.zero;
        [SerializeField] Vector2 secondPlayerPos = Vector2.zero;
        [SerializeField] Vector2[] wallPos = null;
        [SerializeField] Vector2[] npcPos = null;
        [Header("Prefabs")]
        [SerializeField] GameObject firstPlayer = null;
        [SerializeField] GameObject secondPlayer = null;
        [SerializeField] GameObject wall = null;
        [SerializeField] GameObject[] npcsObj = null;
        [Header("Parents")]
        [SerializeField] Transform players = null;
        [SerializeField] Transform walls = null;
        [SerializeField] Transform bullets = null;
        [SerializeField] Transform npcs = null;
        [Header("Audios")]
        [SerializeField] AudioClip inicioDeTurno = null, inicioGameplay = null, fimDeTurno = null, fimDaPartida = null;
        [Header("Stats")]
        public int turnosMax = 5;
        public int turnosJogado = 1;
        public int firstPlayerScore = 0, secondPlayerScore = 0;
        private Timer timer;
        private void Start() {
            timer = new Timer(matchTime);
            timer.OnComplete += GameEndTurn;
            GameTurnBegin();
        }
        private void Update() {
            if (StateController.Instance.currentState == States.GAME_UPDATE) timer.Update();
            elapsedTime = timer.elapsed;
        }
        public void GameTurnBegin(){
            StartCoroutine(WaitForBegin());
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
        public void GameEndTurn(){
            StartCoroutine(WaitForEnd());
        }
        IEnumerator WaitForBegin(){
            StateController.Instance.ChangeState(States.GAME_WAIT);
            SoundController.Instance.SetSfx(inicioDeTurno);

            yield return new WaitForSeconds(beginWaitTime);

            SoundController.Instance.SetSfx(inicioGameplay);
            StateController.Instance.ChangeState(States.GAME_UPDATE);
        }
        IEnumerator WaitForEnd(){
            StateController.Instance.ChangeState(States.GAME_WAIT);
            bool isGameEnd = CheckForEndGame();
            if(isGameEnd){
                SoundController.Instance.SetSfx(fimDaPartida);
                yield return new WaitForSeconds(endGameWaitTime);
                DuelEndgame.winner = WinnerName();
                DuelEndgame.winnerPoints = WinnerPlayer();
                if(WinnerName() == "Corisco"){
                    UnityEngine.SceneManagement.SceneManager.LoadScene(7);
                }else{
                    UnityEngine.SceneManagement.SceneManager.LoadScene(8);
                }
            }else{
                SoundController.Instance.SetSfx(fimDeTurno);
                yield return new WaitForSeconds(endWaitTime);
                ResetGame();
            }
        }
            
        public void ResetGame(){
            var players = FindObjectsOfType<DuelistPlayer>();
            foreach(var p in players){
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
            timer.Reset();
            GameTurnBegin();
        }
        string WinnerName(){
            if(firstPlayerScore > secondPlayerScore){
                return "Antonio das mortes";
            }else{
                return "Corisco";
            }
        }
        int WinnerPlayer(){
            if(firstPlayerScore > secondPlayerScore){
                return firstPlayerScore;
            }else{
                return secondPlayerScore;
            }
        }
        bool CheckForEndGame(){

            if (scoreEndsMatch){
                int minWins = (int)Mathf.Ceil(turnosMax / 2f); // Minimal Points Necessary for Victory
                bool scoreReached = (firstPlayerScore >= minWins || secondPlayerScore >= minWins);
                if (turnosJogado >= turnosMax || scoreReached){
                    SoundController.Instance.SetSfx(fimDaPartida);
                    SoundController.Instance.StopOst();
                    return true;
                }
            }
            else{
                if (turnosJogado >= turnosMax){
                    SoundController.Instance.SetSfx(fimDaPartida);
                    SoundController.Instance.StopOst();
                    return true;
                }
            }
            return false;
        }
    }
}
