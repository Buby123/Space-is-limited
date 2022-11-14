using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles the behavior of a door
/// </summary>
public class Door : MonoBehaviour
{
    [SerializeField] bool listenToButtons = true;
    [SerializeField] bool listenToLevers = true;
    bool opened = false;

    [SerializeField] List<int> ListenToIDs = new List<int>();
    Collider DoorCollider;

    private void Awake()
    {
        DoorCollider = gameObject.GetComponent<Collider>();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        if (listenToButtons)
            EventManager.Instance.OnPushButton += TestButtonTrigger;

        if (listenToLevers)
            EventManager.Instance.OnPullLevel += TestLeverTrigger;
    }

    private void TestButtonTrigger(int triggerId)
    {
        if (ListenToIDs.Any(id => id == triggerId))
        {
            OpenDoor();
        }
    }

    private void TestLeverTrigger(int triggerId, bool direction)
    {
        if (ListenToIDs.Any(id => id == triggerId))
        {
            if (direction)
                OpenDoor();
            else
                CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (!opened)
        {
            opened = true;
            gameObject.SetActive(false);
        }
    }

    public void CloseDoor()
    {
        if (opened)
        {
            opened = false;
            gameObject.SetActive(true);
        }
    }
}
