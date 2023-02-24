using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Checkpoint;

/// <summary>
/// Controller for SceneManagement and State of the Game
/// while outside of the main game
/// </summary>
public class OutgameManager : Singleton<OutgameManager>
{
    //[SerializeField] private CheckPointData currentCheckpoint;

    #region Variables
    [SerializeField] private string menuSceneName = "MainMenu";
    [SerializeField] private string optionsSceneName = "OptionsMenu";
    [SerializeField] private string gameSceneName = "Game";
    #endregion

    #region Propertys

    private bool _gameIsRunning = true;
    public bool gameIsRunning
    {
        get { return _gameIsRunning; }
    }
    #endregion
    /// <summary>
    /// Starts the game if it is not already loaded
    /// </summary>
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        #if UNITY_EDITOR
        //Test game for purpose of testing
        if (SceneManager.GetActiveScene().name.Equals(gameSceneName) && !IngameManager.Instance.gameStarted)
        {
            PauseGame();
            IngameManager.Instance.StartGame();
        }
        #endif
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
    /// <summary>
    /// Load another Scene out of the game, instead of the current scene
    /// </summary>
    /// <param name="loadScene">new scene, that should be active</param>
    public void LoadOtherScene(MainScenes loadScene)
    {
        //Save the data of the scene
        if (SceneManager.GetActiveScene().name.Equals(gameSceneName))
        {
            Debug.Log("Save Current Scene");
            GameObject.Destroy(IngameManager.Instance.gameObject);
        }

        //Load the menu
        switch (loadScene)
        {
            case MainScenes.Game:
                PauseGame();
                SceneManager.LoadScene(gameSceneName);
                IngameManager.Instance.StartGame();
                break;
            case MainScenes.MainMenu:
                SceneManager.LoadScene(menuSceneName);
                break;
        }

        ResumeGame();
    }

    /// <summary>
    /// Loads our OptionsMenu (another scene) additively. This way there is only one Menu which is accessible from everywhere.
    /// </summary>
    public void LoadOptionsMenu()
    {
        SceneManager.LoadSceneAsync(optionsSceneName, LoadSceneMode.Additive);
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

    public enum MainScenes
    {
        MainMenu,
        Game
    }
}
