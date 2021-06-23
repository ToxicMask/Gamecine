using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RythumProto.Timer
{
    public class Timer : MonoBehaviour
    {

        public TimerTarget[] targetList;

        public float timerValue = 10f;
        float timerLeft = 10f;

        public bool autoStart = true;
        public bool loop = false;

        // Start is called before the first frame update
        private void Start()
        {
            // Set Timer
            timerLeft = timerValue;

            //Deactivate if
            if (!autoStart) this.enabled = false;
        }

        // Update is called once per frame
        private void Update()
        {
            //Update Timer
            timerLeft -= Time.deltaTime;

            if (timerLeft < 0)
            {

                foreach (TimerTarget t in targetList)
                {
                    t.TimeOut(); // Set Tick for each element
                }
                


                if (loop) timerLeft = timerValue; // Reset Value

                else this.enabled = false; // Disable Component
            }
        }

        public void StartTime()
        {
            timerLeft = timerValue; // Set all Values as New Time

            this.enabled = true;
        }

        public void StartTime(int newTime)
        {
            timerLeft = timerValue = newTime; // Set all Values as New Time

            this.enabled = true;
        }

        public void ResetTime()
        {
            
            timerLeft = timerValue;

            this.enabled = true;
        }

    }
}
