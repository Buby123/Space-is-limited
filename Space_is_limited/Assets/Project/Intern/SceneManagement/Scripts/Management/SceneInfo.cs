using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Gives the Ingame Manager infos about the position of the player
/// Helps to create a dynamic loading of the game
/// </summary>
public class SceneInfo : MonoBehaviour
{
    #region Objects
    [SerializeField] private string[] NearbyScenes;
    #endregion

    /// <summary>
    /// Gives the ingame manager a hint that the scene is loaded and looks if the player is in the room
    /// </summary>
    private void OnEnable()
    {
        Debug.Log("SceneInfo: " + gameObject.scene.name + " started");
        IngameManager.Instance.AddActiveScene(this);
    }

    #region GetMethods
    /// <summary>
    /// Get the name of the scene
    /// </summary>
    /// <returns>Scenename</returns>
    public string GetSceneName()
    {
        return gameObject.scene.name;
    }
    #endregion

    #region CollisionDetection
    /// <summary>
    /// Proves manualy if the target is in the scene
    /// Is only interesting, if the scene got loaded as the first scene
    /// </summary>
    /// <param name="Target">Target to check</param>
    [ContextMenu("Manual Check Collision")]
    public bool ManualCheckCollision(GameObject Target)
    {
        var PCollider = Target.GetComponent<Collider2D>();
        var SceneCollider = gameObject.GetComponent<Collider2D>();
        return Physics2D.IsTouching(PCollider, SceneCollider);
    }

    /// <summary>
    /// Event if player enters the room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetToMainRoom();
        }
    }
    #endregion

    public void SetToMainRoom()
    {
        Debug.Log("Set to main scene " + GetSceneName());
        IngameManager.Instance.LoadRoom(GetSceneName(), NearbyScenes.ToHashSet());
    }
}
