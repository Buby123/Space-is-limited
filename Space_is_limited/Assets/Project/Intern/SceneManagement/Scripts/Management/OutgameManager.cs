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
    public bool gameIsRunning { get; private set; } = true;
    #endregion

    #region UnityFunctions
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
    #endregion

    #region PausingAndDeath
    /// <summary>
    /// Pauses the game by set its speed to zero
    /// </summary>
    [ContextMenu("Pause")]
    public void PauseGame()
    {
        gameIsRunning = false;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes the game by set its speed to one
    /// </summary>
    [ContextMenu("Resume")]
    public void ResumeGame()
    {
        gameIsRunning = true;
        Time.timeScale = 1f;

    }

    /// <summary>
    /// When the method is called the player dies
    /// the death animation will be played
    /// and the last checkpoint will be loaded
    /// </summary>
    public void TriggerDeath()
    {
        gameIsRunning = false;
        DeathHandler.Instance.TriggerDeath(Death);
    }

    /// <summary>
    /// When the method is called the player dies
    /// and the last checkpoint will be loaded
    /// </summary>
    private void Death()
    {
        IngameManager.Instance.ResetToCheckpoint();
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
