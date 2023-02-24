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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(SwapKey))
                IngameManager.Instance.OpenSingleScene(SceneName);
        }
    }
}
