using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public interface IDataPersistence<T>
{
    public string IDName { get; protected set; }

    // Loads the data from the file
    void LoadData(T data);
    
    // Saves the data to the file
    void SaveData(T data);
}