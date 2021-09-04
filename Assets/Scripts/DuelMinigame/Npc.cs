using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameComponents;

namespace DuelProto.Scenary{
    public class Npc : MonoBehaviour
    {
        public string npcName;
        public float speed;
        public Vector2 firstPos;
        public Vector2 endPos;
        List<Vector2> positions;
        int current;
        private AnimationControl2D animControl;
        private void Start() {
            positions = new List<Vector2>();
            positions.Add(firstPos);
            positions.Add(endPos);
            if (!animControl) animControl = GetComponent<AnimationControl2D>();
        }
        private void Update() {

            if (StateController.Instance.currentState != States.GAME_UPDATE) return;

            if (Vector2.Distance(positions[current], transform.position) <= .1f){
                current++;
                if(current >= positions.Count){
                    current = 0;
                }
                if (animControl) FlipAnimation();
            }
            transform.position = Vector2.MoveTowards(transform.position, positions[current], Time.deltaTime * speed);
        }

        private void FlipAnimation()
        {
            int currentAnimation = current % 2;

            string[] animStates = { "Walk_Left", "Walk_Right" };

            animControl.ChangeState(animStates[currentAnimation]);
        }

    }
}
