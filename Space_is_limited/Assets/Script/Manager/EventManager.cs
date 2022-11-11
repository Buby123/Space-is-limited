using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    #region Events
    public event Action<int> OnPushButton;
    public void PushButton(int target)
    {
        OnPushButton?.Invoke(target);
    }

    public event Action<int, bool> OnPullLevel;
    public void PullLever(int target, bool direction)
    {
        OnPullLevel?.Invoke(target, direction);
    }
    #endregion
}
