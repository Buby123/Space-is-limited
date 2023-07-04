using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;

namespace Project.UI
{
    /// <summary>
    /// Performs the settings changes the user selects in the Options Menu.
    /// Also does some utility for providing the Options menu in the first place.
    /// </summary>
    public class OptionsMenu : MonoBehaviour
    {
        #region Objects
        [SerializeField] private string[] actionTranslator;
        [SerializeField] private TMP_Text[] usedKeysTextFields;
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle vSyncToggle;
        [SerializeField] private GameObject fadeOutImage;
        [SerializeField] private GameObject infoBox;
        [SerializeField] private TMP_Text infoBoxText;

        DataOptions Data = new DataOptions();
        #endregion

        /// <summary>
        /// Every time the user loads the Options-Menu, we need to load the current setting at the beginning.
        /// And loads them from the storage
        /// </summary>
        void Start()
        {
            Data.LoadData();
            PlayerInput.Instance.Data.LoadData();
            Data.ApplyOptions();
            InitGraphics();
        }

        /// <summary>
        /// This is done for Graphics, selected keys and the sound settings.
        /// </summary>
        public void InitGraphics()
        {
            // Update the Graphics
            dropdown.value = Data.ScreenModeInt;
            vSyncToggle.isOn = Data.VSyncCount == 1;

            // Update the selected keys
            for (int i = 0; i < usedKeysTextFields.Length; i++)
            {
                usedKeysTextFields[i].text = "" + PlayerInput.Instance.Data.GetKeyCodeOfAction(actionTranslator[i]);
            }

            // Update the sound
            volumeSlider.value = Data.Volume;
        }

        #region Key Change
        /// <summary>
        /// The Update of this script checks after any Keys pressed.
        /// Only triggered if the user started the process of selecting a new Key.
        /// It waits until the user selected an valid key and then calls the set key function
        /// </summary>
        /// <param name="actionNumber">Number of the action field</param>

        private IEnumerator WaitForKey(int actionNumber)
        {
            bool keyValid = false;
            KeyCode lastKeyPressed = KeyCode.None;

            // Wait for a valid key
            do
            {
                // Wait for a key
                yield return new WaitUntil(() => Input.anyKeyDown);

                if (GetValidKey(out lastKeyPressed, actionNumber))
                {
                    if (KeyCode.None == lastKeyPressed)
                    {
                        yield break;
                    }

                    keyValid = true;
                }
            } while (!keyValid);

            ChangeToThisKey(actionNumber, lastKeyPressed);
        }

        /// <summary>
        /// Returns true if the user selected a valid key.
        /// Returns a Key if the user pressed one and is it not already in use.
        /// Warning it can return true and None if the user pressed the same key as before.
        /// </summary>
        /// <param name="ValidKey">Key that is Valid, is None when not valid or if it is the same key as before</param>
        /// <param name="actionNumber">Number of the action field</param>
        /// <returns></returns>
        public bool GetValidKey(out KeyCode ValidKey, int actionNumber)
        {
            ValidKey = KeyCode.None;

            // The user selected a non valid key
            if (!GetPressedKey(out ValidKey))
            {
                return false;
            }

            // The user selected the key from before
            var action = actionTranslator[actionNumber];

            if (ValidKey == PlayerInput.Instance.Data.GetKeyCodeOfAction(action))
            {
                infoBox.SetActive(false);
                fadeOutImage.SetActive(false);
                ValidKey = KeyCode.None;
                return true;
            }

            // If the user selected a key that is already in use, we need to inform him.
            if (!PlayerInput.Instance.Data.IsKeyCodeAvailable(ValidKey))
            {
                infoBox.SetActive(true);
                infoBoxText.text = "\"" + ValidKey + "\" is already in use!";
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a Key pressed by the user
        /// If no key was pressed it returns None and false.
        /// </summary>
        /// <param name="KeyPressed">Key pressed by the user or none</param>
        /// <returns></returns>
        private bool GetPressedKey(out KeyCode KeyPressed)
        {
            // Check if the key is valid
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    KeyPressed = keyCode;
                    return true;
                }
            }

            KeyPressed = KeyCode.None;
            return false;
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
            StartCoroutine(WaitForKey(whichAction));
        }

        /// <summary>
        /// When the user has selected a key, this function processes the decision.
        /// </summary>
        /// <param name="actionNumber">Actionnumber of the key that is replaced</param>
        /// <param name="newKey">New KeyCode for the place</param>
        private void ChangeToThisKey(int actionNumber, KeyCode newKey)
        {
            Debug.Log("This Key was now selected: " + newKey);

            var action = actionTranslator[actionNumber];

            // If the user selected a valid key, we can change it.
            PlayerInput.Instance.Data.SetKeyCodeOfAction(action, newKey);
            usedKeysTextFields[actionNumber].text = "" + newKey;

            infoBox.SetActive(false);
            fadeOutImage.SetActive(false);
        }
        #endregion

        #region Make Settings
        /// <summary>
        /// This function is called when the user changes the Volume slider.
        /// The new value needs to be read and must then be set as Sound-Volume.
        /// </summary>
        public void ChangeVolumeSettings()
        {
            Debug.Log("User changes the Sound Volume!");

            Data.Volume = volumeSlider.value;
        }

        /// <summary>
        /// Is called if the user changes the vSync settings. Sets the new value.
        /// </summary>
        public void ChangeVSyncSettings(bool isOn)
        {
            Data.VSyncCount = isOn ? 1 : 0;
        }

        /// <summary>
        /// This Function changes the display settings to a user selected choice.
        /// The choice is made in a drop-dowm menu.
        /// </summary>
        public void ChangeDisplaySettings()
        {
            Debug.Log("Window mode is changed by user!");

            Data.ScreenModeInt = dropdown.value; //Implementation for reading the actual value is missing!
        }
        #endregion

        /// <summary>
        /// This Functions unloads the additively Loaded OptionsMenu. The player gets back where he was before.
        /// </summary>
        public void UnloadOptionsMenu()
        {
            SceneManager.UnloadSceneAsync("OptionsMenu");
        }

    }
}