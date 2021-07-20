using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duelist.Ammo{
    public class DuelAmmoSpawner : MonoBehaviour
    {
        [SerializeField] GameObject ammo;
        [SerializeField] Transform[] ammoCima, ammoBaixo;
        [SerializeField] Transform holder;
        [SerializeField] float timeToSpawn = 5f;
        [SerializeField] AudioClip ammoSpawn;
        Timer timer;
        private void Start() {
            timer = new Timer(timeToSpawn);
            timer.OnComplete += SpawnAmmo;
            timer.OnComplete += timer.Reset;
        }
        private void Update() {
            timer.Update();
        }

        void SpawnAmmo(){
            var random = Random.Range(0, ammoCima.Length);
            SoundController.Instance.SetSfx(ammoSpawn);
            if(Random.value <= .5){
                Instantiate(ammo, ammoCima[random].position, Quaternion.identity, holder);
            }else{
                Instantiate(ammo, ammoBaixo[random].position, Quaternion.identity, holder);
            }
        }
    }
}
