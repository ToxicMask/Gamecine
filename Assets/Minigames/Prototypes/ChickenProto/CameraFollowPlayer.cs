using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChickenPrototype.Player;

public class CameraFollowPlayer : MonoBehaviour
{

    public Player playerScript;

    public Transform[] cameraPositions; 

    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (playerScript)
        {
            playerTransform = playerScript.transform;
        }
    }

    private void Update()
    {
        if (playerTransform)
        {
            transform.position = playerTransform.position * Vector2.right;
        }
    }

    // Update is called once per frame
    public void MoveTo(int cameraPositionIndex)
    {
        transform.position = cameraPositions[cameraPositionIndex].position;
    } 
}
