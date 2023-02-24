using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the behavior of a door
/// </summary>
public class Door : MonoBehaviour
{
    #region Variables
    [Tooltip("Activates if the door listens to button events")]
    [SerializeField] bool listenToButtons = true;
    [Tooltip("Activates if the door listens to lever events")]
    [SerializeField] bool listenToLevers = true;
    [Tooltip("Invert Inputs")]
    [SerializeField] bool invert = false;
    bool opened = false;
    #endregion

    #region Objects
    [Tooltip("IDs to which the door listens")]
    [SerializeField] List<int> ListenToIDs = new List<int>();
    Collider DoorCollider;
    #endregion

    #region Initialization
    /// <summary>
    /// Initializes the events and the collider of the door
    /// </summary>
    private void Awake()
    {
        DoorCollider = gameObject.GetComponent<Collider>();
        SubscribeToEvents();
    }

    /// <summary>
    /// Subscribes to the button and lever events
    /// </summary>
    private void SubscribeToEvents()
    {
        if (listenToButtons)
            EventManager.Instance.OnPushButton += TestButtonTrigger;

        if (listenToLevers)
            EventManager.Instance.OnPullLevel += TestLeverTrigger;
    }

    /// <summary>
    /// Test if the Button trigger is related to this door
    /// </summary>
    /// <param name="triggerId">Triggering ID</param>
    private void TestButtonTrigger(int triggerId)
    {
        if (ListenToIDs.Any(id => id == triggerId))
        {
            if (invert)
                CloseDoor();
            else
                OpenDoor();
        }
    }

    /// <summary>
    /// Test if the lever trigger is related to this door
    /// </summary>
    /// <param name="triggerId">Triggering ID</param>
    /// <param name="direction">Direction of the lever</param>
    private void TestLeverTrigger(int triggerId, bool direction)
    {
        if (ListenToIDs.Any(id => id == triggerId))
        {
            if (invert)
                direction = !direction;

            if (direction)
                OpenDoor();
            else
                CloseDoor();
        }
    }
    #endregion

    #region DoorFunctions
    /// <summary>
    /// Opens the door
    /// </summary>
    public void OpenDoor()
    {
        if (!opened)
        {
            opened = true;
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Closes the door
    /// </summary>
    public void CloseDoor()
    {
        if (opened)
        {
            opened = false;
            gameObject.SetActive(true);
        }
    }
    #endregion
}
