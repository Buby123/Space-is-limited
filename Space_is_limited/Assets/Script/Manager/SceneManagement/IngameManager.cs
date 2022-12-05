using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the dynamic loading of the scenes ingame
/// </summary>
public class IngameManager : Singleton<IngameManager>
{
    #region Objects
    [Tooltip("Loaded Scenes")]
    private Dictionary<string, SceneInfo> ActiveScenes = new Dictionary<string, SceneInfo>();
    #endregion

    #region Variables
    [SerializeField] private string StartScene = "Room1";
    #endregion

    /// <summary>
    /// Loads the first scene
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(StartScene, LoadSceneMode.Additive);
        OutgameManager.Instance.ResumeGame();
    }

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
