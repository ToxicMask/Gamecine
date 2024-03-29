﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duelist.Ammo{
    public class DuelAmmoSpawner : MonoBehaviour
    {
        [SerializeField] GameObject ammo = null;
        [SerializeField] Transform[] ammoCima = null, ammoBaixo = null;
        [SerializeField] Transform holder = null;
        [SerializeField] float timeToSpawn = 5f;
        [SerializeField] bool alternateUpAndDown = false;
        [SerializeField] AudioClip ammoSpawn = null;
        Timer timer;
        int spawnOffset = 0;
        
        private void Start() {
            timer = new Timer(timeToSpawn);
            timer.OnComplete += SpawnAmmo;
            timer.OnComplete += timer.Reset;
        }
        private void Update() {
            if (StateController.Instance.currentState == States.GAME_UPDATE) timer.Update();
        }

        void SpawnAmmo(){
            var random = Random.Range(0, ammoCima.Length);
            SoundController.Instance.SetSfx(ammoSpawn);
            if (alternateUpAndDown){
                if (spawnOffset == 0){
                    Instantiate(ammo, ammoCima[random].position, Quaternion.identity, holder);
                }else{
                    Instantiate(ammo, ammoBaixo[random].position, Quaternion.identity, holder);
                }
                spawnOffset = (spawnOffset + 1) % 2; // Offset between Up and Down
            }else{
                if (Random.value <= .5){
                    Instantiate(ammo, ammoCima[random].position, Quaternion.identity, holder);
                }else{
                    Instantiate(ammo, ammoBaixo[random].position, Quaternion.identity, holder);
                }
            }
        }
    }
}
