using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// Performs the settings changes the user selects in the Options Menu.
/// Also does some utility for providing the Options menu in the first place.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    #region Variables
    //[SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private string[] actionTranslator;
    [SerializeField] private TMP_Text[] usedKeysTextFields;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private GameObject fadeOutImage;
    [SerializeField] private GameObject infoBox;
    [SerializeField] private TMP_Text infoBoxText;

    private KeyCode lastKeyPressed;
    private bool lastKeyValid = false;
    private string keyToChange = "";
    private int actionNumber = -1;
    private bool isWaitingForKey = false;
    #endregion

    /// <summary>
    /// Every time the user loads the Options-Menu, we need to load the current setting at the beginning.
    /// 
    /// This is done for Graphics, selected keys and the sound settings.
    /// </summary>
    void Start()
    {
        // Update the Graphics
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow:
                dropdown.value = 0;
                break;
            case FullScreenMode.MaximizedWindow:
                dropdown.value = 1;
                break;
            case FullScreenMode.Windowed:
                dropdown.value = 2;
                break;
            default:
                Debug.LogError("Current Window Mode is not recognized!");
                break;
        }

        if(QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        } else
        {
            vSyncToggle.isOn = true;
        }

        // Update the selected keys
        for (int i = 0; i < usedKeysTextFields.Length; i++)
        {
            usedKeysTextFields[i].text = "" + PlayerInput.Instance.GetKeyCodeOfAction(actionTranslator[i]);
        }

        // Update the sound
        volumeSlider.value = AudioListener.volume;
    }

    /// <summary>
    /// The Update of this script checks after any Keys pressed.
    /// Only triggered if the user started the process of selecting a new Key.
    /// </summary>
    void Update()
    {
        if (!isWaitingForKey)
        {
            return;
        }

        UpdateLastPressedKey();
        if (lastKeyValid)
        {
            lastKeyValid = false;
            ChangeToThisKey(keyToChange, lastKeyPressed);
        }
    }

    /// <summary>
    /// This function checks if there has been any Key's pressed recently. 
    /// If so, the Key is set as 'lastKeyPressed'.
    /// </summary>
    private void UpdateLastPressedKey()
    {
        if (!Input.anyKeyDown)
        {
            return;
        }

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
                Debug.LogError("Invalid display option index: " + optionIndex + "[OptionsMenu.cs.ChangeDisplaySettings()]");
                break;
        }
    }

    /// <summary>
    /// Is called if the user changes the vSync settings. Sets the new value.
    /// </summary>
    public void ChangeVSyncSettings()
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        } else
        {
            QualitySettings.vSyncCount = 0;
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

        actionNumber = whichAction;
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

        if(newKey == PlayerInput.Instance.GetKeyCodeOfAction(action))
        {
            infoBox.SetActive(false);
            fadeOutImage.SetActive(false);
            keyToChange = "";
            isWaitingForKey = false;
            return;
        }

        if (!PlayerInput.Instance.IsKeyCodeAvailable(newKey))
        {
            infoBox.SetActive(true);
            infoBoxText.text = "\"" + newKey + "\" is already in use!";
            return;
        }

        keyToChange = "";
        isWaitingForKey = false;

        PlayerInput.Instance.SetKeyCodeOfAction(action, newKey);

        usedKeysTextFields[actionNumber].text = "" + newKey;

        infoBox.SetActive(false);
        fadeOutImage.SetActive(false);
    }

    /// <summary>
    /// This function is called when the user changes the Volume slider.
    /// The new value needs to be read and must then be set as Sound-Volume.
    /// </summary>
    public void ChangeVolumeSettings()
    {
        Debug.Log("User changes the Sound Volume!");

        AudioListener.volume = volumeSlider.value;
    }

    /// <summary>
    /// This Functions unloads the additively Loaded OptionsMenu. The player gets back where he was before.
    /// </summary>
    public void UnloadOptionsMenu()
    {
        SceneManager.UnloadSceneAsync("OptionsMenu");
    }

}