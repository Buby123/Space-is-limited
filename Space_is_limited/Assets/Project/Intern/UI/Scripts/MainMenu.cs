using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    /// <summary>
    /// MainMenu is a class that controls the MainMenu
    /// </summary>
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
        /// This Function calls the Scene Mangers LoadCheckpoint() function
        /// </summary>
        public void ToGame()
        {
            OutgameManager.Instance.LoadOtherScene(OutgameManager.MainScenes.Game);
        }

        /// <summary>
        /// This Function tells the Outgame Manager to additively load the OptionsMenu
        /// </summary>
        public void ToOptions()
        {
            OutgameManager.Instance.LoadOptionsMenu();
        }
    }
}