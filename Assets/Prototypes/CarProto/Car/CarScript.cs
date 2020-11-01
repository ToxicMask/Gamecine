using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCar.Car
{
    public enum CarState
    {
        Moving,
        Stopped
    }

    public class CarScript : MonoBehaviour
    {
        [SerializeField] CarState currentState = CarState.Stopped;

        // Start is called before the first frame update
        public void ChangeState(CarState newState)
        {
            currentState = newState;
        }
    }
}
