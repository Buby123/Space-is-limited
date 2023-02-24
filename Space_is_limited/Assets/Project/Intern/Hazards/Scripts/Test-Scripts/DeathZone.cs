using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zone, which kills the player
/// </summary>
public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OutgameManager.Instance.TriggerDeath();
        }
    }
}
