using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    [System.Serializable]
    public class CharacterClass 
    {

        public string name = ""; 

        protected CharacterMovement moveScript = null;

        public CharacterClass( CharacterMovement characterMovement)
        {
            moveScript = characterMovement;
        }


        public virtual void Setup()
        {

        }

        public virtual void UpdateLoop()
        {
            
        }

        public virtual void FixedLoop()
        {
            
        }

        public virtual void Exit()
        {

        }

    }

    [System.Serializable]
    public class Pedestrian : CharacterClass
    {
        public Pedestrian(CharacterMovement characterMovement) :base (characterMovement)
        {
            name = "Pedestrian";
        }

        public override void FixedLoop()
        {
            moveScript.StandardMove();
        }
    }
}
