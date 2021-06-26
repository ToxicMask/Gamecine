using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Gun;

/// <summary>
/// Duelist
/// 
/// Includes: 
/// Controller
/// Gun
/// Movement
/// Collision
/// 
/// </summary>
/// 

namespace DuelProto.Duelist
{
    public class DuelistPlayer : MonoBehaviour
    {
        [SerializeField] private float limiteDireita, limiteEsquerda;
        struct InputData
        {
            public Vector2 analog;
            public bool justPressedA;
            public bool justPressedB;
        }

        public class DuelGun
        {
            public DuelGun(Transform bp, GameObject bpf, Transform bf)
            {
                bulletSpawn = bp;
                bulletPreFab = bpf;
                bulletFolder = bf;
            }


            Transform bulletSpawn;
            Transform bulletFolder;
            GameObject bulletPreFab;



            public int bullets = 6;

            public void TryFireGun()
            {
                // Fire Gun
                if (bullets >= 1)
                {
                    Instantiate(bulletPreFab, bulletSpawn.position, Quaternion.Euler(Vector3.zero), bulletFolder);
                    print("Fire!");
                    bullets--;
                }

                else
                {
                    print("Out of Bullets!");
                }
            }
        }

        // Controller
        public int playerNumber = -1;


        // Gun
        public DuelGun mainGun;
        public Transform spawnPoint;
        public Transform bulletFolder;
        public GameObject bulletPrefab;

        // Body
        Rigidbody2D mainBody;

        private void Awake()
        {
            // Auto Get Components
            mainGun = new DuelGun(spawnPoint, bulletPrefab, bulletFolder); // Temp
            mainBody = GetComponent<Rigidbody2D>();
        }


        private void Update()
        {
            if(StateController.Instance.currentState != States.GAME_UPDATE) return;
            InputData currentInput = new InputData();

            DuelController(ref currentInput, playerNumber);
            InputGunFire(mainGun, currentInput.justPressedA);
            MoveCharacter(mainBody, currentInput.analog, this.transform);

        }
        


        private void DuelController(ref InputData input, int playerNumber = -1)
        {

            // Set Player Tag, if not -1, then set multiplayer
            string playerTag = playerNumber == -1 ? "" : playerNumber.ToString();

            // Set Variables
            input.analog = new Vector2(
                Input.GetAxis("Horizontal" + playerTag),    // Horizontal Input   
                Input.GetAxis("Vertical" + playerTag));     // Vertical Input


            input.justPressedA = Input.GetButtonDown("Action Primary" + playerTag);       // Primary Button Pressed
            input.justPressedB = Input.GetButtonDown("Action Secondary" + playerTag);     // Secondary Button Pressed
        }



        private void InputGunFire(in DuelGun gun, in bool Try2Shoot)
        {
            if (Try2Shoot) gun.TryFireGun();
        }

        private void MoveCharacter(in Rigidbody2D mainBody, in Vector2 directionInput, in Transform duelistPos)
        {
            float speedMove = 1.6f;
            Vector2 velocity = speedMove * directionInput.normalized;
            velocity.y = 0;
            var position = duelistPos.position;
            position.x = Mathf.Clamp(position.x, limiteEsquerda, limiteDireita);
            duelistPos.position = position;

            mainBody.velocity = velocity;

        }
        // On collision
        private void OnCollisionEnter2D(Collision2D collision)
        {
            print("!");
            Vector2 collisionDir = collision.GetContact(0).normal;

            transform.position = (Vector2)transform.position  - (collisionDir * -0.075f) ;

        }
    }
}
