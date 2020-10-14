using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sidescroller.Pose
{
    public class SideScrollerPose : MonoBehaviour
    {

        [SerializeField] BoxCollider2D fighterCollision;

        [SerializeField] bool crouching = false;

        // Standing Info
        private Dictionary<string, Vector2> standingCollisionInfo;
        private Dictionary<string, Vector2> crouchCollisionInfo;

        private void Awake()
        {
            fighterCollision = GetComponent<BoxCollider2D>();

            // Set Dictionary Values
            SetDictionaryValues();
        }

        #region Set Dictionary Values

        void SetDictionaryValues()
        {
            // Vectors
            
            Vector2 sizeStanding = new Vector2(0.3f, 1.5f);
            Vector2 sizeCrouch = new Vector2(0.3f, 0.75f);


            // Standing Info
            standingCollisionInfo = new Dictionary<string, Vector2>();
            standingCollisionInfo.Add("size", sizeStanding);

            crouchCollisionInfo = new Dictionary<string, Vector2>(); 
            crouchCollisionInfo.Add("size", sizeCrouch);
        }

        #endregion


        public void StandUp() {

            crouching = false;

            UpdateCollision(crouching);
        }

        public void Crouch()
        {
            crouching = true;

            UpdateCollision(crouching);
        }

        private void UpdateCollision(bool c)
        {
            if (c)
            {
                //Crouching info
                fighterCollision.size = crouchCollisionInfo["size"];

            }

            else
            {
                //Standing Info
                fighterCollision.size = standingCollisionInfo["size"];
                
            }

            // Update Offset
            fighterCollision.offset = new Vector2(fighterCollision.offset.x, fighterCollision.size.y * 0.5f);
        }

        public bool IsCrouching()
        {
            return crouching;
        }
    }
}
