﻿using System.Collections;
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
        [SerializeField] AudioClip inicioDeTurno, inicioGameplay, fimDeTurno, fimDaPartida;
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
            StartCoroutine("WaitForEnd");
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
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
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
        bool CheckForEndGame(){
            if(turnosJogado >= turnosMax){
                SoundController.Instance.SetSfx(fimDaPartida);
                SoundController.Instance.StopOst();
                return true;
            }
            return false;
        }
    }
}
