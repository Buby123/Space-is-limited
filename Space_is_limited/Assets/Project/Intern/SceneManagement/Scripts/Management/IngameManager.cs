using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Checkpoint;

/// <summary>
/// Manages the dynamic loading of the scenes ingame
/// </summary>
public class IngameManager : Singleton<IngameManager>
{
    #region Objects
    [Tooltip("Loaded Scenes")]
    private Dictionary<string, SceneInfo> ActiveScenes = new Dictionary<string, SceneInfo>();

    public bool gameStarted { get; set; } = false;
    #endregion

    #region Propertys
    [field: SerializeField] public CheckPointData CurrentCheckPoint { get; set; } = new CheckPointData("Room1", new Vector3(-10.75f, -4.38f, -0.04553845f));
    #endregion

    #region Begin
    private void Start()
    {
        Debug.Log("Hello World");
    }

    /// <summary>
    /// Loads the first scene
    /// </summary>
    public void StartGame()
    {
        Debug.Log("Try to start game");

        if (gameStarted)
            return;

        Debug.Log("Start game");

        gameStarted = true;
        ResetToCheckpoint();
        OutgameManager.Instance.ResumeGame();
    }
    #endregion

    #region SceneFunctions
    /// <summary>
    /// Close every enviroment scene and loads a new one
    /// </summary>
    /// <param name="SceneName">Scene Name the name of the scene which should be opened</param>
    public void OpenSingleScene(string SceneName, Action<AsyncOperation> OnFinishedOperation = null)
    {
        CloseScenes();
        var Callback = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
        if (OnFinishedOperation != null)
            Callback.completed += OnFinishedOperation;
    }

    /// <summary>
    /// Close every scene that is loaded
    /// </summary>
    private void CloseScenes()
    {
        foreach (var entry in ActiveScenes)
        {
            SceneManager.UnloadSceneAsync(entry.Key);
        }

        ActiveScenes.Clear();
    }

    [ContextMenu("Reset")]
    public void ResetToCheckpoint()
    {
        CurrentCheckPoint.LoadCheckpoint();
    }
    #endregion

    #region DynamicSceneLoading
    /// <summary>
    /// Adds a scene info to the stack
    /// Helps to notice the active scenes
    /// </summary>
    /// <param name="NewScene">SceneInfo</param>
    public void AddActiveScene(SceneInfo NewScene)
    {
        ActiveScenes.Add(NewScene.GetSceneName(), NewScene);
    }

    /// <summary>
    /// Loads a single scene, if the scene is not already loaded
    /// </summary>
    /// <param name="NewRoom">Name of Scene</param>
    public void LoadRoom(string NewRoom)
    {
        if (!ActiveScenes.ContainsKey(NewRoom))
        {
            SceneManager.LoadSceneAsync(NewRoom, LoadSceneMode.Additive);
            return;
        }
    }

    /// <summary>
    /// Deletes a single scene, if it is avaible and is not occupied by the player
    /// </summary>
    /// <param name="DelRoom">Name of Scene</param>
    public void UnloadRoom(string DelRoom)
    {
        if (ActiveScenes.ContainsKey(DelRoom))
        {
            if (!ActiveScenes[DelRoom].OccupiedByPlayer())
            {
                SceneManager.UnloadSceneAsync(DelRoom);
                ActiveScenes.Remove(DelRoom);
            }
        } else
        {
            Debug.LogError("Scene is not existent");
        }
    }
    #endregion
}
