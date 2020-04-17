using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sidescroller.Movement;
using Sidescroller.Fighting;

namespace Sidescroller.Control
{
  
    public class SideScrollerController : MonoBehaviour
    {
        [SerializeField][Range(1,2)] int playerNumber = 1;

        // Components
        SideScrollerMover moverScript;
        SideScrollerFighter fighterScript;

        // Start is called before the first frame update
        void Start()
        {
            // Auto Get
            moverScript = GetComponent<SideScrollerMover>();
            fighterScript = GetComponent<SideScrollerFighter>();
        }

        // Update is called once per frame
        void Update()
        {
            ProcessInput(playerNumber);
        }
        
        private void ProcessInput(int playerID)
        {

            //
            string playerTag = playerID.ToString();

            if (Input.GetAxis("Horizontal" + playerTag) != 0)
            {
                moverScript.Walk(Input.GetAxis("Horizontal" + playerTag));
            }
            if (Input.GetButtonDown("Action Primary" + playerTag))
            {
                fighterScript.AttackBasic();
            }

        }
    } }
