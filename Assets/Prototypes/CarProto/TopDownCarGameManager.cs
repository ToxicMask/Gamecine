using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TopDownCar.Car;
using TopDownCar.Inventory;


namespace TopDownCar.GameManager
{
    public class TopDownCarGameManager : MonoBehaviour
    {

        public GameObject playerObject = null;

        public int coinTarget = 5;



        // Update is called once per frame
        void Update()
        {
            // Check Player Defeat
            if (!(playerObject)) print("Defeat");

            // Check Player Victory
            if (playerObject.GetComponent<CarInventory>())
            {
                if (playerObject.GetComponent<CarInventory>().coinNumber >= coinTarget)
                {
                    print("Victory!");
                    Destroy(gameObject);
                }
            }
        }
    }
}
