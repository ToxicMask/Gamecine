using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DuelProto.Duelist;


namespace DuelProto.Gun
{
    public class Bullet : MonoBehaviour
    {

        Rigidbody2D mainBody;

        public Vector2 direction = Vector2.down;

        private void Awake()
        {
            mainBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Movement(mainBody, direction, 2f);

            // Destroy after 3
            if (Mathf.Abs(transform.position.y) > 3f) Destroy(gameObject);
        }

        void Movement(Rigidbody2D mainBody, Vector2 direction, float speed)
        {
            mainBody.velocity = direction * speed;
        }
    }
}
