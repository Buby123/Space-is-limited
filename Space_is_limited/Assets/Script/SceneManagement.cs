using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for Scenemanagement, can load checkpoints of the game
/// </summary>
public class SceneManagement : Singleton<SceneManagement>
{
    [SerializeField] private CheckPointData currentCheckpoint;

    [SerializeField] private int menuSceneNumber = 0;

    private void Start()
    {
        LoadCheckpointData();
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Initializes the currentCheckpoint
    /// </summary>
    private void LoadCheckpointData()
    {
        var loadedCheckpoint = SaveLoadData.Instance.Load<CheckPointData>("currentCheckpoint", false);

        if (loadedCheckpoint != null)
        {
            currentCheckpoint = loadedCheckpoint;
        } else
        {
            SaveCheckpoint(new CheckPointData());
        }
    }

    /// <summary>
    /// Sets a new Checkpoint
    /// </summary>
    /// <param name="newCheckpoint">Data from Checkpoint</param>
    public void SaveCheckpoint(CheckPointData newCheckpoint)
    {
        currentCheckpoint.activeCheckpoint = newCheckpoint.activeCheckpoint;
        currentCheckpoint.activeSceneName = newCheckpoint.activeSceneName;
        SaveLoadData.Instance.Save(currentCheckpoint, "currentCheckpoint", false);
    }

    /// <summary>
    /// Loads the scene of the current Checkpoint and inits the Checkpoint
    /// </summary>
    [ContextMenu("Load Checkpoint")]
    public void LoadCheckpoint()
    {
        ChangeToScene(currentCheckpoint.activeSceneName);
    }

    /// <summary>
    /// Loads the Scene with the Name: sceneName
    /// </summary>
    /// <param name="sceneName">Name of new Scene</param>
    private void ChangeToScene(string sceneName)
    {
        //Wait for loading Scene Details

        //Loading the Scene
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Load the menu and saves the current data
    /// </summary>
    [ContextMenu("Load Menu")]
    public void LoadMenu()
    {
        //Save the data of the scene

        //Load the menu
        SceneManager.LoadScene(menuSceneNumber);
    }
}

[System.Serializable]
public class CheckPointData
{
    public string activeSceneName = "LittleJumpAndRunScene";
    public int activeCheckpoint = 0;
}
