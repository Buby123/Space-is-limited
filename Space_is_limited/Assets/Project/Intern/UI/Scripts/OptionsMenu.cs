using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    #region Variables
    //[SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private string[] actionTranslator;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private GameObject fadeOutImage;
    [SerializeField] private GameObject infoBox;

    private KeyCode lastKeyPressed;
    private bool lastKeyValid = false;
    private string keyToChange = "";
    private bool isWaitingForKey = false;
    #endregion

    /// <summary>
    /// The Update of this script checks after any Keys pressed.
    /// Only triggered if the user started the process of selecting a new Key.
    /// </summary>
    void Update()
    {
        if (isWaitingForKey)
        {
            UpdateLastPressedKey();
            if(lastKeyValid)
            {
                ChangeToThisKey(keyToChange, lastKeyPressed);
                lastKeyValid = false;
                keyToChange = "";
                isWaitingForKey = false;
            }
        }
    }

    /// <summary>
    /// This function checks if there has been any Key's pressed recently. 
    /// If so, the Key is set as 'lastKeyPressed'.
    /// </summary>
    private void UpdateLastPressedKey()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    lastKeyPressed = keyCode;
                    lastKeyValid = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// This Function changes the display settings to a user selected choice.
    /// The choice is made in a drop-dowm menu.
    /// </summary>
    public void ChangeDisplaySettings()
    {
        Debug.Log("Window mode is changed by user!");

        int optionIndex = dropdown.value; //Implementation for reading the actual value is missing!

        switch (optionIndex)
        {
            case 0: // Fullscreen
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 1: // Borderless window
                Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 2: // Windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                break;
            default:
                Debug.LogError("Invalid display option index: " + optionIndex);
                break;
        }
    }

    /// <summary>
    /// Changes a previously specified key.
    /// The Key is translated from an integer with a Array.
    /// </summary>
    /// <param name="whichAction"></param>
    public void ChangeKeyBinding(int whichAction)
    {
        Debug.Log("User changes a Key! This one: " + actionTranslator[whichAction]);

        fadeOutImage.SetActive(true);
        keyToChange = actionTranslator[whichAction];
        isWaitingForKey = true;
    }

    /// <summary>
    /// When the user has selected a key, this function processes the decision.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="newKey"></param>
    private void ChangeToThisKey(string action, KeyCode newKey)
    {
        Debug.Log("This Key was now selected: " + newKey);
        fadeOutImage.SetActive(false);
    }

    /// <summary>
    /// This function is called when the user changes the Volume slider.
    /// The new value needs to be read and must then be set as Sound-Volume.
    /// </summary>
    public void ChangeVolumeSettings()
    {
        Debug.Log("User changes the Sound Volume!");
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