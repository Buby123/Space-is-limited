using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class OutgameData<T> : IDataPersistence
    {
        [System.NonSerialized] private T Data = default;
        public string DataKey { get; private set; } = "";
        public bool Loaded { get; private set; } = false;

        /// <summary>
        /// Set the default value of the save object and register the save and load function
        /// </summary>
        protected virtual void Initialize(T ChildClass, string Datakey)
        {
            this.DataKey = Datakey;
            this.Data = ChildClass;
        }

        /// <summary>
        /// Load the data of the scene
        /// </summary>
        public void LoadData()
        {
            Loaded = true;

            SaveGameManager.Load(Data, DataKey, false);
        }

        /// <summary>
        /// Saves the data of the object
        /// </summary>
        public void SaveData()
        {
            if (!Loaded)
            {
                Debug.LogWarning("Data is not set properly");
            }

            SaveGameManager.Save(Data, DataKey, false);
        }
    }
}
