using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;
using DuelProto.Scenary;


namespace DuelProto.Gun
{
    public class Bullet : MonoBehaviour
    {

        Rigidbody2D mainBody;
        public AudioClip hitSound;
        public AudioClip wallHitSound;
        public AudioClip npcHitSound;
        public Vector2 direction = Vector2.down;
        private float speed = 5f;
        public int playerNumber;
        private void Awake()
        {
            mainBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // End Bullet in End Turn

            if (StateController.Instance.currentState != States.GAME_UPDATE) Destroy(this.gameObject);


            Movement(mainBody, direction, speed);

            // Destroy after Border
            if (Mathf.Abs(transform.position.y) > 10f) Destroy(gameObject);
        }

        void Movement(Rigidbody2D mainBody, Vector2 direction, float speed)
        {
            mainBody.velocity = direction * speed;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Wall>()) Destroy(gameObject);

            var health = collision.GetComponent<Health>();
            var duelist = collision.GetComponent<DuelistPlayer>();
            var npc = collision.GetComponent<Npc>();
            if(health != null){
                if(health.IsDead()){
                    if(wallHitSound != null)SoundController.Instance.SetSfx(wallHitSound);
                    Destroy(collision.gameObject);
                    return;
                }
                health.Current--;
            }
            if(npc != null){
                SoundController.Instance.SetSfx(npcHitSound);
                if(playerNumber== 2){
                    Duel.Manager.DuelManager.Instance.firstPlayerScore++;
                }else{
                    Duel.Manager.DuelManager.Instance.secondPlayerScore++; 
                }
                Duel.Manager.DuelManager.Instance.GameEndTurn();
            }
            if(duelist != null){
                SoundController.Instance.SetSfx(hitSound);
                if(duelist.playerNumber== 2){
                Duel.Manager.DuelManager.Instance.firstPlayerScore++;
                }else{
                    Duel.Manager.DuelManager.Instance.secondPlayerScore++; 
                }
                Duel.Manager.DuelManager.Instance.GameEndTurn();
            }
            Destroy(this.gameObject);
        }
    }
}
