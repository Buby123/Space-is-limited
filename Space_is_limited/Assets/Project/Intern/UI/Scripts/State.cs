using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component, that can be added to a UI Component, if there the should be controlled easily (turn on / off)
/// </summary>
public class State : MonoBehaviour
{
    [Tooltip("Name of the state")]
    [SerializeField] private int state;
    [Tooltip("Toggles whether the gameObject should be toggled on/off")]
    [SerializeField] private bool changeActivityOfGameObjekt = true;

    /// <summary>
    /// Subscribes to the StateHandler
    /// </summary>
    public void Awake()
    {
        StateHandler.Instance.Subscribe(state, TestForInvokeState, TestForCloseState);
    }

    /// <summary>
    /// Tests when an event is casted, whether the event is related to this state.
    /// If so it activates the gameObject
    /// </summary>
    /// <param name="targetedState"></param>
    private void TestForInvokeState(int targetedState)
    {
        if (state == targetedState)
        {
            if (changeActivityOfGameObjekt)
                gameObject.SetActive(true);

            InvokeState();
        }
    }

    /// <summary>
    /// Tests when an event is casted, whether the event is related to this state.
    /// If so it deactivates the gameObject
    /// </summary>
    /// <param name="targetedState"></param>
    private void TestForCloseState(int targetedState)
    {
        if(state == targetedState)
        {
            if (changeActivityOfGameObjekt)
                gameObject.SetActive(false);

            CloseState();
        }
    }

    /// <summary>
    /// Is activated when the state is called
    /// </summary>
    protected virtual void InvokeState()
    {

    }

    /// <summary>
    /// Is activated when the state gets closed
    /// </summary>
    protected virtual void CloseState()
    {

    }
}
