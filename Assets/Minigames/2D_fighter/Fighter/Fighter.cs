using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    #region Input Variables

    [SerializeField]
    private Vector2 movement_dir = new Vector2();

    //[SerializeField]
    //private bool attack_act = false, defense_act = false;

    #endregion

    #region Components
    [SerializeField]
    CharacterController char_control;

    #endregion

    #region Movement Parameters

    private float walk_speed = .6f;

    #endregion

    // Get parameters
    private void Start()
    {
        // Auto Get Components
        char_control = GetComponent<CharacterController>();

    }

    //  Function to set input each frame
    private void Update()
    {
        // Horizontal Movement
        movement_dir.x = Input.GetAxis("Horizontal");


    }


    // Function to set Character Action each physics
    private void FixedUpdate()
    {

        Vector2 linear_velocity = movement_dir * walk_speed;

        char_control.Move(linear_velocity * Time.deltaTime);
    }

}
