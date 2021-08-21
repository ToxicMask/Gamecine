using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Player;


namespace ChickenGameplay.Camera
{
    public class CameraFollowPlayer : MonoBehaviour
    {

        [SerializeField] float tweenTime = .6f;

        public void MoveTo(Vector2 newPosition)
        {
            LeanTween.moveLocal(gameObject, newPosition, tweenTime).setEaseOutCubic();

            //transform.position = newPosition;
        }
    }
}
