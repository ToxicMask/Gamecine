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

        public Vector2 direction = Vector2.down;
        private float speed = 5f;

        private void Awake()
        {
            mainBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement(mainBody, direction, speed);

            // Destroy after 3
            //if (Mathf.Abs(transform.position.y) > 3.2f) Destroy(gameObject);
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
            if(health != null){
                if(health.IsDead()){
                    if(wallHitSound != null)SoundController.Instance.SetSfx(wallHitSound);
                    Destroy(collision.gameObject);
                    return;
                }
                health.Current--;
            }
            if(duelist != null){
                SoundController.Instance.SetSfx(hitSound);
                if(duelist.playerNumber== 2){
                Duel.Manager.DuelManager.Instance.firstPlayerScore++;
                }else{
                    Duel.Manager.DuelManager.Instance.secondPlayerScore++; 
                }
                Duel.Manager.DuelManager.Instance.ResetGame();
            }
            Destroy(this.gameObject);
        }
    }
}
