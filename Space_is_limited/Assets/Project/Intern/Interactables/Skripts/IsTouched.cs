using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.InteractionHelpers
{
    /// <summary>
    /// Gives the information if the player touches the box
    /// </summary>
    public class IsTouched : MonoBehaviour
    {
        private bool _playerTouchesBox = false;

        public bool State => _playerTouchesBox;

        #region Player Touches
        /// <summary>
        /// Check if player touches the box
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _playerTouchesBox = true;
            }
        }

        /// <summary>
        /// Check if player touches the box
        /// </summary>
        /// <param name="other"></param>
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                _playerTouchesBox = false;
            }
        }
        #endregion
    }
}

