using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
/// <summary>
/// Handles the data of one checkpoint and sets the main checkpoint to itself if player arrives
/// </summary>
public class Checkpoint : MonoBehaviour
{        
    [field: SerializeField] public GameObject CheckPointOrigin { get; private set; }
    public CheckPointData Data { get; private set; }

    /// <summary>
    /// Init CheckPointData
    /// </summary>
    private void Start()
    {
        var sceneName = gameObject.scene.name;
        Data = new CheckPointData(sceneName, CheckPointOrigin.transform.position);
    }

    /// <summary>
    /// Test for player collision and activate if it is there
    /// </summary>
    /// <param name="collision">Other Collider</param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player arrived at Checkpoint");
            ActivateCheckpoint();
        }
    }

    /// <summary>
    /// Activates the checkpoint
    /// </summary>
    private void ActivateCheckpoint()
    {
        IngameManager.Instance.CurrentCheckPoint = Data;
    }

    [System.Serializable]
    /// <summary>
    /// Data to set the player to this checkpoint
    /// </summary>
    public class CheckPointData
    {
        [field: SerializeField] public string sceneName { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        /// <summary>
        /// Init the Data
        /// </summary>
        /// <param name="sceneName">Scene Name where the checkpoint is</param>
        /// <param name="Position">Position where the checkpoint is</param>
        public CheckPointData(string sceneName, Vector3 Position)
        {
            this.sceneName = sceneName;
            this.Position = Position;
        }

        /// <summary>
        /// Loads the specific room and sets the player to the position
        /// </summary>
        public void LoadCheckpoint()
        {
            // Load Scene
            IngameManager.Instance.OpenSingleSceneAsync(sceneName);
            SetPlayersPosition();
        }

        /// <summary>
        /// Sets the player to the position
        /// </summary>
        private void SetPlayersPosition()
        {
            // Find player
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                Debug.LogWarning("Failed to find a player");
                return;
            }

            player.transform.position = Position;
        }
    }
}
