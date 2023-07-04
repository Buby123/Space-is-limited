using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.SceneManagement;

namespace Project.Hazards
{
    /// <summary>
    /// Zone, which kills the player
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        /// <summary>
        /// Trigger death when a object with the tag "Player" enters the field
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OutgameManager.Instance.TriggerDeath();
            }
        }
    }
}

