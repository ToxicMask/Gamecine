using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenGameplay.Chicken;
using ChickenGameplay.GameManager;

namespace ChickenGameplay.Navigate
{
    public class DeadLine : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<RunnerChicken>())
            {
                GAME_STATE result = GAME_STATE.FAILURE;
                ChickenLevelManager.instance.ChangeState(result);
            }
        }

    }
}
