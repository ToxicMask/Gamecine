using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;


namespace DuelProto.Bullet
{
    public class Bullet : MonoBehaviour
    {

        Rigidbody2D mainBody;

        private void Awake()
        {
            mainBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement(mainBody, Vector2.down, 2f);
        }

        void Movement(Rigidbody2D mainBody, Vector2 direction, float speed)
        {

        }
    }
}
