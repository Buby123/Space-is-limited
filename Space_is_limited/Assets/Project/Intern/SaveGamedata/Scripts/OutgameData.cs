using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class OutgameData<T> : IDataPersistence
    {
        private T Data = default;
        public string DataKey { get; private set; } = "";
        public bool Loaded { get; private set; } = false;

        /// <summary>
        /// Set the default value of the save object and register the save and load function
        /// </summary>
        protected virtual void Initialize(T ChildClass, bool deleteOnDeath)
        {
            this.DataKey = DataKey;
            this.Data = ChildClass;
        }

        /// <summary>
        /// Load the data of the scene
        /// </summary>
        void IDataPersistence.LoadData()
        {
            if (!Loaded)
            {
                Debug.LogWarning("Data is not set properly");
            }

            SaveGameManager.Instance.Load(Data, DataKey, false);
        }

        /// <summary>
        /// Saves the data of the object
        /// </summary>
        void IDataPersistence.SaveData()
        {
            Loaded = true;

            SaveGameManager.Instance.Save(Data, DataKey, false);
        }
    }
}
