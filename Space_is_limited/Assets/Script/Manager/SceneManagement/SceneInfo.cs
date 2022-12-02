using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool playerInArea = false;
    private bool geladen = false;
    private bool soll_gel_w = false;
    #endregion

    /// <summary>
    /// Guves the ingame manager a hint that the scene is loaded and looks if the player is in the room
    /// </summary>
    private void Start()
    {
        GameObject.Find("GameManager").GetComponent<IngameManager>().AddActiveScene(this);
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
        return playerInArea;
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
        soll_gel_w = load;
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
        if (soll_gel_w != geladen)
        {
            geladen = soll_gel_w;

            foreach(string sceneName in NearbyScenes)
            {
                if (soll_gel_w)
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
    private void ManualCheckCollision()
    {
        var PCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();

        if (gameObject.GetComponent<Collider2D>().IsTouchingLayers())
        {
            playerInArea = true;
            TryLoadNeighbors(true);
        }
        else
        {
            playerInArea = false;
            TryLoadNeighbors(false);
        }
    }

    /// <summary>
    /// Event if player enters the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerInArea = true;
            TryLoadNeighbors(true);
        }
    }

    /// <summary>
    /// Event if player leaves the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            playerInArea = false;
            TryLoadNeighbors(false);
        }
    }
    #endregion
}
