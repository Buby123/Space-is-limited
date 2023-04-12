using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Intern.UI
{
    /// <summary>
    /// Controls the EnergyBar
    /// </summary>
    public class EnergyBar : MonoBehaviour
    {
        [Tooltip("The slider, that is used to display the energy")]
        [SerializeField] Slider slider;

        /// <summary>
        /// Sets the max energy of the slider
        /// </summary>
        /// <param name="newMaxEnergy"> max Energy to be set</param>
        public void setMaxEnergy(int newMaxEnergy)
        {
            slider.maxValue = newMaxEnergy;
            slider.value = newMaxEnergy;
        }

        /// <summary>
        /// Sets the energy of the slider
        /// </summary>
        /// <param name="energy">Energy to be set</param>
        public void setEnergy(int energy)
        {
            slider.value = Mathf.Clamp(energy, 0, slider.maxValue);
        }
    }
}
