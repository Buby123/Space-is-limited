using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Opens a new scene when the player enters the trigger
/// </summary>
public class DoorExample : MonoBehaviour
{
    [SerializeField] private string SceneName = "Backroom";
    [SerializeField] private KeyCode SwapKey;

    #region Detect Player in Area
    bool playerInArea = false;

    /// <summary>
    /// Set the playerInArea bool to true when the player enters the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = true;
        }
    }

    /// <summary>
    /// Set the playerInArea bool to false when the player leaves the trigger
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }
    #endregion

    /// <summary>
    /// Switch to the new scene when the player presses the key
    /// </summary>
    private void Update()
    {
        if (playerInArea && Input.GetKeyDown(SwapKey))
            IngameManager.Instance.OpenSingleScene(SceneName);
    }
}
