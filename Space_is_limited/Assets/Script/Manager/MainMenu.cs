using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// This function quits the game if it was build (not working in Unity preview)
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// This Function calls the Scene Mangers LoadMenu() function
    /// </summary>
    public void ToMenu()
    {
        SceneManagement.Instance.LoadMenu();
    }

    /// <summary>
    /// TTis Function calls the Scene Mangers LoadCheckpoint() function
    /// </summary>
    public void ToGame()
    {
        SceneManagement.Instance.LoadCheckpoint();
    }
}