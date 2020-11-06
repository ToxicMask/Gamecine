using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.TopDownShooter
{
    public class TopDownShooter : MonoBehaviour
    {

        public GameObject prefabBullet;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // Mouse World Location
            //Vector3 mouseLocation = Input.mousePosition;
            //mouseLocation.z = -(Camera.main.transform.position.z);
            //mouseLocation = Camera.main.ScreenToWorldPoint(mouseLocation);

            int vInput = (int)Input.GetAxis("Vertical");           // Vertical Input
            int hInput = (int)Input.GetAxis("Horizontal");         // Horizontal Input
            bool pButton = Input.GetButtonDown("Action Primary");   // Primary Button Pressed
            bool sButton = Input.GetButtonDown("Action Secondary"); // Secondary Button Pressed


            //Rotate
            transform.Rotate(Vector3.back * hInput * Time.deltaTime * 180f);

            //Move
            transform.position += (transform.up * vInput * Time.deltaTime * 2f);


            if (pButton)
            {
                Instantiate(prefabBullet, transform.position, transform.rotation);
            }
        }
    }
}
