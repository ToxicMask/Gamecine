using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownCar.Inventory;

namespace TopDownCar.Item
{
    public class Coin : MonoBehaviour
    {

        private int value = 1;

        private void OnTriggerEnter2D(Collider2D other)
        {
            CarInventory cInv = other.GetComponent<CarInventory>();

            if (cInv)
            {
                cInv.AddCoin(value);
                Destroy(gameObject);
            }
        }
    }
}
