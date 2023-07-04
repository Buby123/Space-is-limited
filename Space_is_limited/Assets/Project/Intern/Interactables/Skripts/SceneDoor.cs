using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.InteractionHelpers;
using UnityEngine.Events;
using Project.SceneManagement;
//using Project.Player;

namespace Project.Interaction
{
    /// <summary>
    /// Opens a new scene when the player enters the trigger
    /// </summary>
    public class SceneDoor : InteractionArea
    {
        [SerializeField] private string SceneName = "Backroom";

        private void Start()
        {
            InitAction(OnInteraction);
        }

        /// <summary>
        /// Switch to the new scene when the player presses the key
        /// </summary>
        public void OnInteraction()
        {
            IngameManager.Instance.OpenSingleSceneAsync(SceneName);
        }
    }
}

