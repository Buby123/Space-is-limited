using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the events of levers and buttons in the game
/// </summary>
public class EventManager : Singleton<EventManager>
{
    #region Events
    public event Action<int> OnPushButton;
    /// <summary>
    /// Triggers event for any action with only one input
    /// </summary>
    /// <param name="id">ID of the button</param>
    public void PushButton(int id)
    {
        OnPushButton?.Invoke(id);
    }

    public event Action<int, bool> OnPullLevel;
    /// <summary>
    /// Triggers event for any Action with bool input
    /// </summary>
    /// <param name="id">ID of the lever</param>
    /// <param name="direction">New direction of the lever</param>
    public void PullLever(int id, bool direction)
    {
        OnPullLevel?.Invoke(id, direction);
    }
    #endregion
}
