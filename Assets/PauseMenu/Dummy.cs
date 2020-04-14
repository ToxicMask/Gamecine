using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PauseTest
{
    public class Dummy : MonoBehaviour
    {

        [SerializeField] bool rotate = false;
        [SerializeField] float rotationSpeed = 2.0f;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (rotate)
            {
                transform.Rotate(Vector3.forward * rotationSpeed);
            }

        }
    }
}
