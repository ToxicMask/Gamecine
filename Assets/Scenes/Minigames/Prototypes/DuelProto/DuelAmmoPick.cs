using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Duelist.Ammo{
    public class DuelAmmoPick : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            Debug.Log("hit");
            var duelist = other.GetComponent<DuelProto.Duelist.DuelistPlayer>();
            if(duelist != null){
                if(duelist.mainGun.bullets == 6) return;
                duelist.mainGun.bullets++;
                Destroy(this.gameObject);
            }
        }
    }
}
