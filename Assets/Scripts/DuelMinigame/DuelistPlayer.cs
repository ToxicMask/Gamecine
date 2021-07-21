using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Gun;
using GameComponents;

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
        public AudioClip ShootSound;
        public AudioClip failedShootSound;
        public AudioClip passos;
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

            // Start Amunition
            public int bullets = 5;

            public void TryFireGun(AudioClip succesShoot, AudioClip failedShoot, int playerNumber)
            {

                // Fire Gun
                if (bullets >= 1)
                {
                    SoundController.Instance.SetSfx(succesShoot);
                    var bullet = Instantiate(bulletPreFab, bulletSpawn.position, Quaternion.Euler(Vector3.zero), bulletFolder);
                    bullet.GetComponent<Bullet>().playerNumber = playerNumber;
                    bullets--;
                    //Debug print("Fire!");
                }

                else
                {
                    SoundController.Instance.SetSfx(failedShoot);
                    //Debug  print("Out of Bullets!");
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

        // Animation Control
        AnimationControl2D animControl = null;

        private void Awake()
        {
            // Auto Get Components
            mainGun = new DuelGun(spawnPoint, bulletPrefab, bulletFolder);
            mainBody = GetComponent<Rigidbody2D>();
            animControl = GetComponent<AnimationControl2D>();
        }


        private void Update()
        {
            // Stop Update on Game Wait
            if (StateController.Instance.currentState != States.GAME_UPDATE){
                mainBody.velocity = Vector2.zero;
                if (animControl) animControl.ChangeState("Idle");
                //else print("NO ANIMATION CONTROL");
                return;
            }

            InputData currentInput = new InputData();

            DuelController(ref currentInput, playerNumber);
            InputGunFire(mainGun, currentInput.justPressedA);
            MoveCharacter(mainBody, currentInput, this.transform);

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



        private void InputGunFire(DuelGun gun, bool Try2Shoot)
        {
            if (Try2Shoot) {
                gun.TryFireGun(ShootSound, failedShootSound, playerNumber);
                // Animation
                if (animControl) animControl.ChangeState("Shoot");
                else print("NO ANIMATION CONTROL");
            }
        }

        private void MoveCharacter(Rigidbody2D mainBody, InputData currentInput , Transform duelistPos)
        {
            Vector2 directionInput = currentInput.analog;
            float speedMove = 1.6f;
            Vector2 velocity = speedMove * directionInput.normalized;
            velocity.y = 0;
            var position = duelistPos.position;
            position.x = Mathf.Clamp(position.x, limiteEsquerda, limiteDireita);
            duelistPos.position = position;

            mainBody.velocity = velocity;

            // Animation
            if (animControl)
            {
                if (velocity == Vector2.zero) animControl.ChangeState("Idle");
                else animControl.ChangeState("Walk");
            }
            //else print("NO ANIMATION CONTROL");
        }
    }
}
