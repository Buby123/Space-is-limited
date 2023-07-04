using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    /// <summary>
    /// Gives options to save objects under a specific filename in the ressources
    /// It defines the folder structure to save the data, by using the SaveLoadData Class
    /// </summary>
    public static class SaveGameManager
    {
        [Tooltip("Folder where the data will be saved and loaded from, only for Data thats not saved when the player dies")]
        static string SessionDataFolder = "TemporalSceneData";

        /// <summary>
        /// Saves the data of the object
        /// </summary>
        /// <typeparam name="T">Objecttype saved</typeparam>
        /// <param name="obj">Object itself</param>
        /// <param name="filename">Name of the file aka the key</param>
        /// <param name="deleteOnDeath">Whether it should be in the delete folder</param>
        public static void Save<T>(T obj, string filename, bool deleteOnDeath)
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
        /// <param name="deleteOnDeath">Whether it should be in the delete folder</param>
        public static void Load<T>(T obj, string filename, bool deleteOnDeath)
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
        public static void DeleteOnDeath()
        {
            SaveLoadData.Instance.DeleteFolder(SessionDataFolder);
        }
    }
}
