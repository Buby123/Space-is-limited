using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Handles the behavior of a door
    /// </summary>
    public class SceneDoor : MonoBehaviour
    {
        #region Variables
        [Tooltip("Activates if the door listens to button events")]
        [SerializeField] bool listenToButtons = true;
        [Tooltip("Activates if the door listens to lever events")]
        [SerializeField] bool listenToLevers = true;
        [Tooltip("Invert Inputs")]
        [SerializeField] bool invert = false;

        bool _opened = false;
        #endregion

        #region Objects
        [Tooltip("IDs to which the door listens")]
        [SerializeField] List<int> ListenToIDs = new();
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes the events and the collider of the door
        /// </summary>
        private void Awake()
        {
            SubscribeToEvents();
        }

        /// <summary>
        /// Subscribes to the button and lever events
        /// </summary>
        private void SubscribeToEvents()
        {
            if (listenToButtons)
                EventManager.Instance.OnPushButton += (id) => FilterTriggers(id, true);

            if (listenToLevers)
                EventManager.Instance.OnPullLevel += FilterTriggers;
        }

        /// <summary>
        /// Test if the lever trigger is related to this door
        /// </summary>
        /// <param name="triggerId">Triggering ID</param>
        /// <param name="open">Wether to open or close the door</param>
        private void FilterTriggers(int triggerId, bool open)
        {
            if (ListenToIDs.Any(id => id == triggerId))
            {
                ChangeOpenState(open);
            }
        }
        #endregion

        #region DoorFunctions
        /// <summary>
        /// Opens or closes the door
        /// </summary>
        public void ChangeOpenState(bool open)
        {
            if (invert)
                open = !open;

            if (open == _opened)
                return;

            gameObject.SetActive(!open);
            _opened = open;
        }
        #endregion
    }

}
