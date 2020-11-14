using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GuideCharacter
{
    [System.Serializable]
    public enum ClassName
    {
        Null,
        Pedestrian,
        Guard,
    }


    [System.Serializable]
    public class CharacterClass 
    {

        public ClassName name = ClassName.Null; 

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

        public virtual void OnTriggerEnter(Collider2D collider)
        {

        }

        public virtual void OnTriggerStay(Collider2D collider)
        {

        }

        public virtual void OnTriggerExit(Collider2D collider)
        {

        }

        public virtual void Exit()
        {

        }

    }

    [System.Serializable]
    public class Pedestrian : CharacterClass
    {
        public Pedestrian(CharacterMovement characterMovement) :base(characterMovement)
        {
            name = ClassName.Pedestrian;
        }

        public override void FixedLoop()
        {
            moveScript.StandardMove();
        }

        public override void Exit()
        {
            moveScript.Stop();
        }
    }

    [System.Serializable]
    public class Guard : CharacterClass
    {
        public Guard(CharacterMovement characterMovement) : base(characterMovement)
        {
            name = ClassName.Guard;
        }

        public override void Setup()
        {
            moveScript.OnlyFall();
        }

        public override void UpdateLoop()
        {
            moveScript.OnlyFall();
        }

        public override void OnTriggerEnter(Collider2D collider)
        {
            // Try to get AutoChar Movement
            CharacterMovement move = collider.GetComponent<CharacterMovement>();

            if (move)
            {
                move.InvertWalkDirection();
            }
        }
    }
}
