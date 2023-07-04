using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SaveSystem
{
    /// <summary>
    /// This interface is used to save and load data
    /// </summary>
    public interface IDataPersistence
    {
        /// <summary>
        /// Loads the data from the file
        /// </summary>
        public void LoadData();

        /// <summary>
        /// Saves the data to the file
        /// </summary>
        public void SaveData();
    }
}
