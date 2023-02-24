using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates an instance, which is accessible from everywhere
/// </summary>
/// <typeparam name="T">Singleton Skript</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    #region Objekten
    /// <summary>
    /// Accessible instance
    /// </summary>
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                instance = obj.AddComponent<T>();
            }
            return instance;
        }
    }
    #endregion

    #region unityFunktionen
    /// <summary>
    /// Creates an instance at the start of the game and deletes unnecessary ones
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
