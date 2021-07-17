using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duelist.Ammo{
    public class DuelAmmoPick : MonoBehaviour
    {
        [SerializeField] AudioClip pickupSound;
        private void OnTriggerEnter2D(Collider2D other) {
            //Debug.Log("hit");
            var duelist = other.GetComponent<DuelProto.Duelist.DuelistPlayer>();
            if(duelist != null){
                if(duelist.mainGun.bullets == 6) return;
                SoundController.Instance.SetSfx(pickupSound);
                duelist.mainGun.bullets++;
                Destroy(this.gameObject);
            }
        }
    }
}
