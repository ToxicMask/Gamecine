using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter {
    public class Timer : MonoBehaviour
    {

        public static Timer current = null;

        public float time_seconds = 0;

        public float max_seconds = 60;

        public float TimeLeft => max_seconds - time_seconds;

        // Start is called before the first frame update
        void Awake()
        {
            current = this;

            time_seconds = 0;
        }

        void Update()
        {
            if (PauseSystem.gameIsPaused) return;

            time_seconds += Time.deltaTime;

            int time_left =  (int) Mathf.Clamp (TimeLeft, 0, 10000000000000000);

            //print(((time_left)).ToString());

            if (time_left <= 0)
            {
                print("Time Out!");
                this.enabled = false;
                LevelManager.current.EndLevel();
            }
        }
    }
}
