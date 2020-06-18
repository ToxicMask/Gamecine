using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sidescroller.Fighting
{



    public class PlayerHealthBar : MonoBehaviour
    {
        Slider healthSlider;

        private void Start()
        {
            // Auto Get

            healthSlider = GetComponent<Slider>();
        }

        public void SetHealth(int currentValue)
        {
            if (healthSlider == null) return;

            healthSlider.value = currentValue;
        }
    }

}
