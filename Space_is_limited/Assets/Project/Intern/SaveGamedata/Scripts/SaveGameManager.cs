using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    public class SaveGameManager : Singleton<SaveGameManager>
    {
        [Tooltip("Folder where the data will be saved and loaded from, only for Data thats not saved when the player dies")]
        [SerializeField] string SessionDataFolder = "TemporalSceneData";

        /// <summary>
        /// Saves the data of the object
        /// </summary>
        /// <typeparam name="T">Objecttype saved</typeparam>
        /// <param name="obj">Object itself</param>
        /// <param name="filename">Name of the file aka the key</param>
        /// <param name="deleteOnDeath">Wether it should be in the delete folder</param>
        public void Save<T>(T obj, string filename, bool deleteOnDeath)
        {
            if (deleteOnDeath)
            {
                filename = SessionDataFolder + Path.DirectorySeparatorChar + filename;
            }
            
            SaveLoadData.Instance.Save(obj, filename, false);
        }

        /// <summary>
        /// Loads the Data to the object
        /// </summary>
        /// <typeparam name="T">Objecttype loaded</typeparam>
        /// <param name="obj">Object itself</param>
        /// <param name="filename">Name of the file aka the key</param>
        /// <param name="deleteOnDeath">Wether it should be in the delete folder</param>
        public void Load<T>(T obj, string filename, bool deleteOnDeath)
        {
            if (deleteOnDeath)
            {
                filename = SessionDataFolder + Path.DirectorySeparatorChar + filename;
            }

            SaveLoadData.Instance.Load(obj, filename, false);
        }

        /// <summary>
        /// Deletes all data that should be deleted on death
        /// </summary>
        public void DeleteOnDeath()
        {
            // Delete the Data that should be deleted on death
            SaveLoadData.Instance.DeleteFolder(SessionDataFolder);
        }
    }
}
