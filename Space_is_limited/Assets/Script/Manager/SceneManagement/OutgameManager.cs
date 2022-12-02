using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for SceneManagement and State of the Game
/// while outside of the main game
/// </summary>
public class OutgameManager : Singleton<OutgameManager>
{
    //[SerializeField] private CheckPointData currentCheckpoint;

    #region Variables
    [SerializeField] private string menuSceneName = "MainMenu";
    [SerializeField] private string optionsSceneName = "";
    [SerializeField] private string gameSceneName = "Game";
    #endregion

    #region Propertys
    private bool _gameIsRunning = true;
    public bool gameIsRunning
    {
        get { return _gameIsRunning; }
    }
    #endregion

    private void Start()
    {
        //LoadCheckpointData();
        DontDestroyOnLoad(gameObject);
    }

    #region Pausing
    /// <summary>
    /// Pauses the game by set its speed to zero
    /// </summary>
    [ContextMenu("Pause")]
    public void PauseGame()
    {
        _gameIsRunning = false;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes the game by set its speed to one
    /// </summary>
    [ContextMenu("Resume")]
    public void ResumeGame()
    {
        _gameIsRunning = true;
        Time.timeScale = 1f;

    }
    #endregion

    #region SceneManagement
    public void LoadOtherScene(MainScenes loadScene)
    {
        //Save the data of the scene
        if (SceneManager.GetActiveScene().name.Equals(gameSceneName))
        {
            Debug.Log("Save Current Scene");
        }

        //Load the menu
        switch (loadScene)
        {
            case MainScenes.Game:
                SceneManager.LoadScene(gameSceneName);
                break;
            case MainScenes.MainMenu:
                SceneManager.LoadScene(menuSceneName);
                break;
            case MainScenes.OptionMenu:
                SceneManager.LoadScene(optionsSceneName);
                break;
        }

        ResumeGame();
    }

    /// <summary>
    /// Stops and Saves the game
    /// </summary>
    public void StopGame()
    {
        //Save the data of the scene
        if (SceneManager.GetActiveScene().name.Equals(gameSceneName))
        {
            Debug.Log("Save Current Scene");
        }

        Application.Quit();
    }
    #endregion

    /*
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
    }*/



    public enum MainScenes
    {
        MainMenu,
        OptionMenu,
        Game
    }
}

/*[System.Serializable]
public class CheckPointData
{
    public string activeSceneName = "LittleJumpAndRunScene";
    public int activeCheckpoint = 0;
}*/
