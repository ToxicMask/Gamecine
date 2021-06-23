using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototypes.TopDownShooter
{
    public class Bullet : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            transform.position += transform.up * 8f * Time.deltaTime;
        }
    }
}
