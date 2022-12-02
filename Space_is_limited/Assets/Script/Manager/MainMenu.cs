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
        OutgameManager.Instance.StopGame();
    }

    /// <summary>
    /// This Function calls the Scene Mangers LoadMenu() function
    /// </summary>
    public void ToMenu()
    {
        OutgameManager.Instance.LoadOtherScene(OutgameManager.MainScenes.MainMenu);
    }

    /// <summary>
    /// TTis Function calls the Scene Mangers LoadCheckpoint() function
    /// </summary>
    public void ToGame()
    {
        OutgameManager.Instance.LoadOtherScene(OutgameManager.MainScenes.Game);
    }
}