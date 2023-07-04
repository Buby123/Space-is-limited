using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.InteractionHelpers
{
    /// <summary>
    /// Adds a event to the interaction key
    /// </summary>    
    public class InteractionArea : MonoBehaviour
    {
        #region Objects
        private UnityAction _ActionInArea;
        private List<UnityAction<bool>> _ToggleFunctions;
        #endregion

        public void InitAction(UnityAction ActionInArea, List<UnityAction<bool>> ToggleFunctions = null)
        {
            _ActionInArea = ActionInArea;
            _ToggleFunctions = ToggleFunctions;
        }

        #region Unity Methods
        /// <summary>
        /// Check if player touches the box
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player entered");
                PlayerInput.Instance.OnInteraction.AddListener(_ActionInArea);
                _ToggleFunctions?.ForEach(action => action(true));
            }
        }

        /// <summary>
        /// Check if player touches the box
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player left");
                PlayerInput.Instance.OnInteraction.RemoveListener(_ActionInArea);
                _ToggleFunctions?.ForEach(action => action(false));
            }
        }
        #endregion
    }
}

