using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all saveable data
/// </summary>
[System.Serializable]
public abstract class SceneData
{
    [field: SerializeField] public string DataKey { get; protected set; } = "";
    [field: SerializeField] public bool deleteOnDeath { get; protected set; } = false;
    public bool loaded { get; protected set; } = false;

    public SceneData(string DataKey, bool deleteOnDeath) => Initialize(DataKey, deleteOnDeath);

    /// <summary>
    /// Set the default value of the save object and register the save and load function
    /// </summary>
    protected virtual void Initialize(string DataKey, bool deleteOnDeath)
    {
        this.DataKey = DataKey;
        this.deleteOnDeath = deleteOnDeath;
        SaveGameManager.Instance.Register(this);
    }

    /// <summary>
    /// Tests if the file is not loaded
    /// Implement here your save function with the key
    /// </summary>
    public virtual void SaveData()
    {
        if (!loaded)
        {
            Debug.LogError("Data is not set properly");
        }
    }

    /// <summary>
    /// Tests if the file is already loaded
    /// Implement here your load function with the key
    /// </summary>
    public virtual void LoadData()
    {
        if (loaded)
        {
            Debug.Log("Data is already loaded");
            return;
        }

        loaded = true;
    }
}
