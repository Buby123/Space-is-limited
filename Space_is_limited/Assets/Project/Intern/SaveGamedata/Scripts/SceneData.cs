using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Base class for all saveable data
    /// </summary>
    [System.Serializable]
    public abstract class SceneData<T> : IDataPersistence
    {
        private T Data = default;
        public string DataKey { get; private set; } = "";
        public bool DeleteOnDeath { get; private set; } = false;
        public bool Loaded { get; private set; } = false;

        /// <summary>
        /// Set the default value of the save object and register the save and load function
        /// </summary>
        protected virtual void Initialize(T ChildClass, string DataKey, bool deleteOnDeath)
        {
            this.DataKey = DataKey;
            this.DeleteOnDeath = deleteOnDeath;
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

            SaveGameManager.Instance.Load(Data, DataKey, DeleteOnDeath);
        }

        /// <summary>
        /// Saves the data of the object
        /// </summary>
        void IDataPersistence.SaveData()
        {
            Loaded = true;

            SaveGameManager.Instance.Save(Data, DataKey, DeleteOnDeath);
        }
    }
}
