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
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ProcessInput();

        }

        private void ProcessInput()
        {
           if (Input.GetAxis("Horizontal")!= 0)
            {
                GetComponent<SideScrollerMover>().Walk(Input.GetAxis("Horizontal"));
            }
           if (Input.GetButtonDown("Action Primary"))
            {
                GetComponent<SideScrollerFighter>().AttackBasic();
            }
        }
    } }
