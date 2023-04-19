using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// This Function changes the display settings to a user selected choice.
    /// The choice is made in a drop-dowm menu.
    /// </summary>
    public void ChangeDisplaySettings()
    {
        int optionIndex = 2; //Implementation for reading the actual value is missing!

        switch (optionIndex)
        {
            case 0: // Fullscreen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1: // Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            case 2: // Borderless window
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            default:
                Debug.LogError("Invalid display option index: " + optionIndex);
                break;
        }
    }

    /// <summary>
    /// This function is called when the user changes the Volume slider.
    /// The new value needs to be read and must then be set as Sound-Volume.
    /// </summary>
    public void ChangeVolumeSettings()
    {
        // Implementation missing
    }

    /// <summary>
    /// This Functions unloads the additively Loaded OptionsMenu. The player gets back where he was before.
    /// </summary>
    public void UnloadOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }

}