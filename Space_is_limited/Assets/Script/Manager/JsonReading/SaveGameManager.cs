using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveGameManager : Singleton<SaveGameManager>
{
    List<SceneData> DataFiles = new List<SceneData>();
    List<string> KeysDeleteOnDeath = new List<string>();

    [Tooltip("Folder where the data will be saved and loaded from, only for Data thats not saved when the player dies")]
    [SerializeField] string SessionDataFolder = "TemporalSceneData";
    
    [Tooltip("Folder where the data will be saved and loaded from, for Data that saved even when the player dies")]
    [SerializeField] string PersistentDataFolder = "PersistentSceneData";

    /// <summary>
    /// Registers a new Datafile for the scene
    /// </summary>
    /// <param name="sceneData">Scene Data Object</param>
    public void Register(SceneData sceneData)
    {
        DataFiles.Add(sceneData);
    }

    /// <summary>
    /// Loads each data file in the scene
    /// </summary>
    public void LoadScene()
    {
        foreach (var dataFile in DataFiles)
        {
            dataFile.LoadData();
        }
    }

    /// <summary>
    /// Saves the current scene with all registered data files in it
    /// </summary>
    public void SaveScene()
    {
        foreach (var dataFile in DataFiles)
        {
            dataFile.SaveData();

            if (dataFile.deleteOnDeath)
            {
                KeysDeleteOnDeath.Add(dataFile.DataKey);
            }
        }
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
