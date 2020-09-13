using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sidescroller.Fighting
{



    public class PlayerHealthBar : MonoBehaviour
    {
        Slider healthSlider;

        private void Awake()
        {
            // Auto Get

            healthSlider = GetComponent<Slider>();
        }

        public void SetHealth(int currentValue)
        {
            if (healthSlider == null) return;

            healthSlider.value = currentValue;
        }

        public void SetMaxHealth(int newValue)
        {
            if (healthSlider == null) return;
            healthSlider.maxValue = newValue;
        }
    }

}
