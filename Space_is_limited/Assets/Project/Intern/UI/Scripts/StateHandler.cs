using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Switches between states and stacks them
/// The StateHandler can give variables to other states to change the interface
/// </summary>
public class StateHandler : Singleton<StateHandler>
{
    #region Objects
    private List<int> StateStack = new List<int>();
    IDictionary<string, OverstateVariable> OverstateVars = new Dictionary<string, OverstateVariable>();
    #endregion

    #region Variables
    [Tooltip("State that should be started on initialization")]
    [SerializeField] private int startState = 0;
    [Tooltip("Toggles whether a state should be started on start")]
    [SerializeField] private bool startStateOnInit = true;
    #endregion

    #region Events
    public event Action<int> OnCloseState;
    private void CloseState(int state)
    {
        OnCloseState?.Invoke(state);
    }

    public event Action<int> OnInvokeState;
    private void InvokeState(int state)
    {
        OnInvokeState?.Invoke(state);
    }
    #endregion

    #region Initialization
    /// <summary>
    /// Initializes the first state
    /// </summary>
    private void Start()
    {
        if(startStateOnInit)
            OpenState(startState);
    }
    
    /// <summary>
    /// Subscribes a new state to the StateHandler
    /// </summary>
    /// <param name="state">Number of the state</param>
    /// <param name="invokeState">Invoke Action</param>
    /// <param name="closeState">Close Action</param>
    public void Subscribe(int state, Action<int> invokeState, Action<int> closeState)
    {
        OnInvokeState += invokeState;
        OnCloseState += closeState;
        OnCloseState(state);
    }
    #endregion

    #region StateHandling
    /// <summary>
    /// Opens a state with the number and adds it to the stack
    /// </summary>
    /// <param name="state">Number of the state</param>
    public void OpenState(int state)
    {
        StateStack.Add(state);
        InvokeState(state);
    }

    /// <summary>
    /// Closes the last opened window (state)
    /// </summary>
    public void Back()
    {
        CloseState(StateStack[StateStack.Count - 1]);
        StateStack.Remove(StateStack.Count - 1);
    }

    /// <summary>
    /// Closes the last opened window and opens another one instead
    /// </summary>
    /// <param name="state">Number of the new state</param>
    public void SwitchTo(int state)
    {
        Back();
        OpenState(state);
    }

    /// <summary>
    /// Closes all opened states
    /// </summary>
    public void Home()
    {
        int count = StateStack.Count - 1;

        while (count > 0)
        {
            CloseState(StateStack[count]);
            StateStack.Remove(count);
            count--;
        }
    }
    #endregion

    #region VariableHandling

    /// <summary>
    /// Adds a variable that will be avaible until called
    /// </summary>
    /// <typeparam name="T">Type of the Value</typeparam>
    /// <param name="indentifier">Name of variable</param>
    /// <param name="Value">Value of variable</param>
    public void AddVariable<T>(string indentifier, T Value)
    {
        var NewVar = new OverstateVariable();
        NewVar.SetValue<T>(Value);
        OverstateVars.Add(indentifier, NewVar);
    }

    /// <summary>
    /// Gets a variable with the chosen type and indentifier
    /// </summary>
    /// <typeparam name="T">Type of variable</typeparam>
    /// <param name="indentifier">Name of variable</param>
    /// <returns></returns>
    public T GetVariable<T>(string indentifier)
    {
        if (OverstateVars.TryGetValue(indentifier, out OverstateVariable Value))
        {
            OverstateVars.Remove(indentifier);
            return Value.GetValue<T>();
        }
        else
        {
            Debug.LogError("No such variable avaible");
            return default;
        }
    }

    /// <summary>
    /// Clears all variables
    /// </summary>
    public void DeleteVariables()
    {
        OverstateVars.Clear();
    }
    #endregion
}

[SerializeField]
public class OverstateVariable
{
    private object Value;
    private Type Type;

    /// <summary>
    /// Sets the value of the Overstate Variable
    /// </summary>
    /// <typeparam name="T">Type of the variable</typeparam>
    /// <param name="_Value">Value of the variable</param>
    public void SetValue<T>(T _Value)
    {
        Value = _Value;
        Type = typeof(T);
    }

    /// <summary>
    /// Returns value of the variable
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    /// <returns>Value of variable</returns>
    public T GetValue<T>()
    {
        if(Type == typeof(T))
        {
            return (T)Value;
        }

        Debug.LogError("Type Error");
        return default;
    }
}
