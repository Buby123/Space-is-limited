using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SaveSystem
{
    /// <summary>
    /// Loads and Saves classes
    /// </summary>
    public class SaveLoadData : Singleton<SaveLoadData>
    {
        private string path = ""; //Path to save during testing
        private string persistantPath = ""; //Path to save while playing

        /// <summary>
        /// Startfunction calls GetPath
        /// </summary>
        private void OnEnable()
        {
            GetPath();
        }

        /// <summary>
        /// Saves a class object as file in serialized form
        /// 
        /// Warning each component you want to save must be serialized
        /// You can use List<> but not Arrays!!!
        /// Dont forget to use [System.Serializeable]
        /// </summary>
        /// <typeparam name="T">Classtype</typeparam>
        /// <param name="saveObject">Classobject that should be saved</param>
        /// <param name="filename">Name as which the file should be saved</param>
        /// <param name="local">If true the file get saved in Unityfolder /Data (set only to true while testing)</param>
        public void Save<T>(T saveObject, string filename, bool local)
        {
            var usedPath = local ? path : persistantPath;

            var data = JsonUtility.ToJson(saveObject);

            using StreamWriter writer = new StreamWriter(usedPath + filename + ".json");
            writer.Write(data);

        }

        /// <summary>
        /// Loads a classobject and returns it
        /// </summary>
        /// <typeparam name="T">Classtype</typeparam>
        /// <param name="filename">Name as which the file was saved</param>
        /// <param name="local">Was the file saved in Unity</param>
        /// <returns>Classobject</returns>
        public void Load<T>(T saveObject, string filename, bool local)
        {
            var usedPath = local ? path : persistantPath + filename + ".json";

            if (File.Exists(usedPath))
            {
                using StreamReader reader = new StreamReader(usedPath);
                var data = reader.ReadToEnd();
                JsonUtility.FromJsonOverwrite(data, saveObject);
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogWarning("File not found " + filename);
#endif
            }
        }

        /// <summary>
        /// Creates the actual paths for the system
        /// </summary>
        private void GetPath()
        {
            persistantPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
            path = Application.dataPath + Path.AltDirectorySeparatorChar + "Data" + Path.AltDirectorySeparatorChar;
        }

        /// <summary>
        /// Deletes a folder and all its content
        /// </summary>
        /// <param name="folder">Name of the folder</param>
        public void DeleteFolder(string folder)
        {
            var usedPath = persistantPath + folder;

            if (Directory.Exists(usedPath))
            {
                Directory.Delete(usedPath, true);
            }
        }
    }
}

