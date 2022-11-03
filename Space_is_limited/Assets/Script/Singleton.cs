using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Erzeugt eine Instanz, welche von Überall aus Zugreifbar ist
/// </summary>
/// <typeparam name="T">Singleton Skript</typeparam>
public class Singleton<T> : MonoBehaviour where T : Component
{
    #region Objekten
    /// <summary>
    /// Zugreifbare Instanz
    /// </summary>
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }
    #endregion

    #region unityFunktionen
    /// <summary>
    /// Erzeugt eine Instanz bei Start des Spiels und löscht Überflüssige
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
