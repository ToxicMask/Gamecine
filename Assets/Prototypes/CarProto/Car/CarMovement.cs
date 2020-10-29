using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownCar.Movement
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D carBody;

        [SerializeField] float speed;

        [SerializeField] float dampSpeed = 0f;

        public Vector2 direction = Vector2.up;

        List<Vector2> dirList = new List<Vector2> { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        // Pysics
        float maxSpeed = 4;
        float minSpeed = .005f;
        float accelSpeed = .005f;
        float breakSpeed = .05f;
        

        private void Start()
        {
            //Reset Values
            direction = Vector2.up;
            // Auto Get
            carBody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            // Damp Speed
            speed -= dampSpeed;

            // Clamp Speed, is not accelaring
            if (speed < minSpeed) speed = 0;

            // Set Velocity
            carBody.velocity = speed * direction;

            //
        }


        public void GoFoward()
        {

            print("Go foward!");
            // Increasses
            speed += accelSpeed;

            // Clamps
            if (speed > maxSpeed) { speed = maxSpeed; }

        }

        public void Break()
        {
            print("Break");
            speed -= breakSpeed;
            
            // Clamp Speed
            if (speed < minSpeed) { speed = 0; }
        }

        public void TurnLeft()
        {

            int currentDir = FindDirectionIndex(direction);

            int newDirIndex = (currentDir - 1) % 4;

            if (newDirIndex == -1) newDirIndex = 3;

            // New Direction value
            Vector2 newDirValue = GetDirectionValue(newDirIndex);

            // Change Direction
            ChangeDirection(newDirValue);

        }

        public void TurnRight()
        {

            int currentDir = FindDirectionIndex(direction);

            int newDirIndex = (currentDir + 1) % 4;

            // New Direction value
            Vector2 newDirValue = GetDirectionValue(newDirIndex);

            // Change Direction
            ChangeDirection(newDirValue);

        }

        private int FindDirectionIndex (Vector2 dir)
        {
            for (int i = 0; i < dirList.Count; i++) {
                if (dir == dirList[i]) return i;
                        }
            return -1;
        }

        private Vector2 GetDirectionValue (int index)
        {
            return dirList[index];
        }

        private void ChangeDirection(Vector2 newDir)
        {
            direction = newDir;
            print(newDir);
        }
    }
}

