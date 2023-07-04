using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI
{
    /// <summary>
    /// Controls the EnergyBar
    /// </summary>
    public class EnergyBar : Singleton<EnergyBar>
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
        /// <returns>True, if the energy was set. False if the energy was not set</returns>
        public void setEnergy(int energy)
        {
            slider.value = Mathf.Clamp(energy, 0, slider.maxValue);
        }

        /// <summary>
        /// Reduces the energy of the slider
        /// </summary>
        /// <param name="energy">Energy to be reduced</param>
        /// <returns>True, if the energy was reduced, false if the energy was not reduced</returns>
        public bool reduceEnergy(int energy)
        {
            if (slider.value - energy < 0)
            {
                return false;
            }
            else
            {
                slider.value -= energy;
                return true;
            }
        }
    }
}
