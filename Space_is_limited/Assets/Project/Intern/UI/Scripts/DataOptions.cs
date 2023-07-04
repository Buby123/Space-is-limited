using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using System;

namespace Project.UI
{
    /// <summary>
    /// DataOptions is a class that holds the options data
    /// </summary>
    [System.Serializable]
    public class DataOptions : OutgameData<DataOptions>
    {
        #region Variables
        [SerializeField] private int vSyncCount = 0;
        [SerializeField] private int screenMode = 0;
        [SerializeField] private float volume = 1f;
        #endregion

        /// <summary>
        /// Gets or sets the vSyncCount
        /// </summary>
        public int VSyncCount
        {
            get => vSyncCount;
            set
            {
                if (value < 0 || value > 1)
                {
                    Debug.LogError("vSyncCount is out of range");
                    return;
                }

                vSyncCount = value;
                ApplyOptions();
            }
        }

        /// <summary>
        /// Gets or sets the screenMode
        /// </summary>
        public int ScreenModeInt
        {
            get => screenMode;
            set
            {
                if (value < 0 || value > 2)
                {
                    Debug.LogError("Screenmode is out of range");
                    return;
                }

                screenMode = value;
                ApplyOptions();
            }
        }

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public float Volume 
        {
            get => volume;
            set
            {
                if (value < 0f || value > 1f)
                {
                    Debug.LogError("Volume value is out of range: " + value + "[OptionsMenu.cs.Volume]");
                    return;
                }

                volume = value;
                ApplyOptions();
            }
        }

        /// <summary>
        /// Gets the screenMode
        /// </summary>
        private FullScreenMode ScreenMode
        {
            get
            {
                switch (screenMode)
                {
                    case 0: // Fullscreen
                        return Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                    case 1: // Borderless window
                        return Screen.fullScreenMode = FullScreenMode.MaximizedWindow;
                    case 2: // Windowed
                        return Screen.fullScreenMode = FullScreenMode.Windowed;
                    default:
                        Debug.LogError("Invalid display option index: " + screenMode + "[OptionsMenu.cs.ChangeDisplaySettings()]");
                        return FullScreenMode.Windowed;
                }
            }
        }

        public DataOptions()
        {
            Initialize(this, "Options");
        }

        /// <summary>
        /// Sets the settings to unity
        /// </summary>
        public void ApplyOptions()
        {
            QualitySettings.vSyncCount = vSyncCount;
            Screen.fullScreenMode = ScreenMode;
            AudioListener.volume = volume;
            SaveData();
        }
    }
}