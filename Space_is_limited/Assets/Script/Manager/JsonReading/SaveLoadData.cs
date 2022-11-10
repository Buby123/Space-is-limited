using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    private void Start()
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

        Debug.Log("Save location is: " + usedPath);

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
    public T Load<T>(string filename, bool local)
    {
        var usedPath = local ? path : persistantPath;

        using StreamReader reader = new StreamReader(usedPath + filename + ".json");
        var data = reader.ReadToEnd();

        return JsonUtility.FromJson<T>(data);
    }

    /// <summary>
    /// Creates the actual paths for the system
    /// </summary>
    private void GetPath()
    {
        persistantPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "Data" + Path.AltDirectorySeparatorChar;
    }
}
