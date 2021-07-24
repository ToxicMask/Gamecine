using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenGameplay.Scenary
{
    public class TilesGismoDraw : MonoBehaviour
    {

        public bool active = false;

        public static Vector2 tileSize = Vector2.one * .25f;
        public static Vector2 tileOffset = Vector2.one * .125f;
        public Vector2Int mapOffset = Vector2Int.zero;
        public Vector2Int mapSize = Vector2Int.one;


        private void OnDrawGizmos()
        {

            if (!active) return;

            Gizmos.color = Color.grey;

            for (int i = 0; i < mapSize.x; i++){
                for (int j = 0; j < mapSize.y; j++){

                    Vector2 totalOffset = new Vector2(
                        mapOffset.x * tileSize.x,
                        mapOffset.y * tileSize.y 
                        );

                    //Sum X
                    totalOffset.x += i * tileSize.x + tileOffset.x;
                    //Sum Y
                    totalOffset.y -= j * tileSize.y + tileOffset.y;

                    Gizmos.DrawWireCube(transform.position + (Vector3)totalOffset, tileSize);
                }
            }
        }
    }
}
