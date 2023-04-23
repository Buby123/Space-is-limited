using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
    private string MainScene = "";

    public bool gameStarted { get; set; } = false;
    #endregion

    #region Propertys
    [field: SerializeField] public CheckPointData CurrentCheckPoint { get; set; } = new CheckPointData("Room1", new Vector3(-10.75f, -4.38f, -0.04553845f));
    #endregion

    #region Begin
    /// <summary>
    /// Loads the first scene
    /// </summary>
    public void StartGame()
    {
        if (gameStarted)
            return;

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
    public void OpenSingleScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        StartCoroutine(SetToMainScene(SceneName));
    }

    /// <summary>
    /// Makes the scene to a main scene when loaded
    /// </summary>
    /// <param name="SceneName">Name of the scene</param>
    /// <returns></returns>
    private IEnumerator SetToMainScene(string SceneName)
    {
        yield return new WaitUntil(() => ActiveScenes.ContainsKey(SceneName));
        ActiveScenes[SceneName].SetToMainRoom();
    }

    [ContextMenu("Reset")]
    public void ResetToCheckpoint()
    {
        CurrentCheckPoint.LoadCheckpoint();
    }
    #endregion

    #region DynamicSceneLoading
    /// <summary>
    /// Loads a room by removing every active scene not in the list or that is the roomname itself
    /// and then adding each on missing
    /// </summary>
    /// <param name="RoomName"></param>
    /// <param name="NearbyScenes"></param>
    public void LoadRoom(string RoomName, HashSet<string> NearbyScenes)
    {
        // Dont load the same room again
        if (RoomName.Equals(MainScene))
        {
            return;
        }

        NearbyScenes.Add(RoomName);
        MainScene = RoomName;

        UnloadRooms(NearbyScenes);
        LoadRooms(NearbyScenes);
    }
    
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
    private void LoadRooms(HashSet<string> NearbyRooms)
    {
        foreach (var NearbyRoom in NearbyRooms)
        {
            if (!ActiveScenes.ContainsKey(NearbyRoom))
            {
                SceneManager.LoadSceneAsync(NearbyRoom, LoadSceneMode.Additive);
            }
        }
    }

    /// <summary>
    /// Deletes all scenes not in the list
    /// </summary>
    /// <param name="NearbyRooms">Name of Scenes that should be active</param>
    private void UnloadRooms(HashSet<string> NearbyRooms)
    {
        List<string> RemovedRooms = new();

        foreach (var LoadedRoom in ActiveScenes.Select(s => s.Key))
        {
            if (!NearbyRooms.Contains(LoadedRoom))
            {
                SceneManager.UnloadSceneAsync(LoadedRoom);
                RemovedRooms.Add(LoadedRoom);
            }
        }

        RemovedRooms.ForEach(r => ActiveScenes.Remove(r));
    }
    #endregion
}
