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

            // TEMP
            UpdateCollision();
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

        }

        public void Crouch()
        {
            crouching = true;
        }

        private void UpdateCollision()
        {
            if (crouching)
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
    }
}
