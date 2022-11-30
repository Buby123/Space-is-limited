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
}