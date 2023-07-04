using UnityEngine;

namespace Project.UI
{
    /// <summary>
    /// DataEnergy is a class that holds the energy data
    /// </summary>
    [System.Serializable]
    public class DataEnergy
    {
        #region Variables
        private int _maxEnergy;
        private int _currentEnergy;
        #endregion

        /// <summary>
        /// Gets or sets the maxEnergy
        /// </summary>
        public int maxEnergy
        {
            get { return _maxEnergy; }
            set { 
                _maxEnergy = value;
                currentEnergy = maxEnergy;
            }
        }

        /// <summary>
        /// Gets or sets the currentEnergy
        /// </summary>
        public int currentEnergy
        {
            get { return _currentEnergy; }
            set { _currentEnergy = Mathf.Clamp(value, 0, maxEnergy); }
        }
    }
}