using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace GuideCharacter
{
    public class PaulistaClassDisplay : MonoBehaviour
    {

        public TMP_Text displayName = null;
        public Image displayIcon = null;

        protected ClassName currentClass = ClassName.Null;


        // Update is called once per frame
        void LateUpdate()
        {

            // Update Class
            currentClass = CursorCommandControl.current.selectedClassName;

            UpdateNameDisplay();
            UpdateIconDisplay();
        }

        private void UpdateNameDisplay()
        {

            string template = "Current Class:\n{0}";

            string newName = currentClass.ToString();

            string newText = string.Format(template, newName);

            displayName.text = newText;
        }

        private void UpdateIconDisplay()
        {
            
            switch (currentClass)
            {
                case ClassName.Pedestrian: displayIcon.color = Color.green; break;
                case ClassName.Guard: displayIcon.color = Color.red; break;
            }

        }
    }
}
