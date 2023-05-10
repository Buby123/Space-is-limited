using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SaveSystem
{
    public interface IDataPersistence
    {
        // Loads the data from the file
        void LoadData();

        // Saves the data to the file
        void SaveData();
    }
}
