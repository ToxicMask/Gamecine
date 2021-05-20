using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCar.Inventory
{
    public class CarInventory : MonoBehaviour
    {
        public int coinNumber { get; private set; }

        // Can add bosts

        //Health
        //Armor

        private void Awake()
        {
            Reset_Inventory();
        }

        private void Reset_Inventory()
        {
            coinNumber = 0;
        }

        public void AddCoin(int value)
        {
            coinNumber += value;
            print(coinNumber);
        }
    }
}
