using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

/// <summary>
/// Activates OnActivateAbility and OnDeactivateAbility as soon as the ability is activated/deactivated
/// </summary>
[System.Serializable]
public class PlayerAbility
{
    private bool active = false;

    /// <summary>
    /// Subscribes to events, to set up the user input
    /// </summary>
    public void OnEnable()
    {
        PlayerInput.Instance.OnSpecialAbility.AddListener(ToggleAbility);
        Debug.Log("Ability enabled");
    }

    public void Update()
    {
        if (active)
            OnUpdateAbility();
    }

    /// <summary>
    /// Unsubscribes from events, to disable the ability
    /// </summary>
    public void OnDisable()
    {
        PlayerInput.Instance.OnSpecialAbility.RemoveListener(ToggleAbility);
    }

    /// <summary>
    /// Is used to toggle the ability on and off
    /// It uses the input to activate the appropriate function
    /// </summary>
    /// <param name="active"></param>
    private void ToggleAbility(bool active)
    {
        this.active = active;

        if (active)
        {
            OnActivateAbility();
        }
        else
        {
            OnDeactivateAbility();
        }
    }

    protected virtual void OnActivateAbility()
    {
        // Write code for activation
    }

    protected virtual void OnDeactivateAbility()
    {
        // Write code for deactivation
    }

    protected virtual void OnUpdateAbility()
    {
        // Write code for update
    }
}
