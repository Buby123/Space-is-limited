using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gives the Ingame Manager Infos about the position of the player
/// Helps to create a dynamic loading of the game
/// </summary>
public class SceneInfo : MonoBehaviour
{
    #region Objects
    [SerializeField] private string[] NearbyScenes;
    #endregion

    #region Variables
    private bool playerInScene = false;
    private bool loaded = false;
    private bool shouldBeLoaded = false;
    #endregion

    /// <summary>
    /// Guves the ingame manager a hint that the scene is loaded and looks if the player is in the room
    /// </summary>
    private void Start()
    {
        Debug.Log("SceneInfo: " + gameObject.scene.name + " started");
        IngameManager.Instance.AddActiveScene(this);
        ManualCheckCollision();
    }

    #region GetMethods
    /// <summary>
    /// Get the name of the scene which is active
    /// </summary>
    /// <returns>Scenename</returns>
    public string GetSceneName()
    {
        return gameObject.scene.name;
    }

    /// <summary>
    /// Gives back if the player is in the scene
    /// </summary>
    /// <returns>Player in Scene?</returns>
    public bool OccupiedByPlayer()
    {
        return playerInScene;
    }
    #endregion

    #region LoadMechanics
    /// <summary>
    /// Loads or unloads the neighbor-rooms of the room
    /// This method uses a delay to ensure performance between chunk borders
    /// </summary>
    /// <param name="load">Load/Unload</param>
    public void TryLoadNeighbors(bool load)
    {
        shouldBeLoaded = load;
        if (load)
        {
            CancelInvoke();
            InstantTryLoad();
        }
        else
        {
            Invoke(nameof(InstantTryLoad), 1f);
        }
        
    }

    /// <summary>
    /// Loads instantly the neighbor-rooms or unloads them
    /// </summary>
    /// <param name="load">Load/Unload</param>
    private void InstantTryLoad()
    {
        if (shouldBeLoaded != loaded)
        {
            loaded = shouldBeLoaded;

            foreach(string sceneName in NearbyScenes)
            {
                if (shouldBeLoaded)
                {
                    IngameManager.Instance.LoadRoom(sceneName);
                } else
                {
                    IngameManager.Instance.UnloadRoom(sceneName);
                }
            }
        }
    }
    #endregion

    #region CollisionDetection
    /// <summary>
    /// Proofs manualy if the player is already in the scene
    /// Is only interisting, if the scene get loaded as first scene
    /// </summary>
    [ContextMenu("Manual Check Collision")]
    public void ManualCheckCollision()
    {
        var PCollider = PlayerController.Instance.transform.Find("Appearance").GetComponent<Collider2D>();
        var SceneCollider = gameObject.GetComponent<Collider2D>();

        playerInScene = Physics2D.IsTouching(PCollider, SceneCollider);
        TryLoadNeighbors(playerInScene);
    }

    /// <summary>
    /// Event if player enters the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInScene = true;
            TryLoadNeighbors(true);
        }
    }

    /// <summary>
    /// Event if player leaves the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInScene = false;
            TryLoadNeighbors(false);
        }
    }
    #endregion
}
