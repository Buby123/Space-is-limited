using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aktiviert OnActivateAbility und OnDeactivateAbility sobald die Fähigkeit aktiviert/deaktiviert wird
/// </summary>
[System.Serializable]
public class PlayerAbility
{
    /// <summary>
    /// Subscribes to events, to set up the user input
    /// </summary>
    public void OnEnable()
    {
        PlayerInput.Instance.OnUse.AddListener(ToggleAbility);
        Debug.Log("Ability enabled");
    }

    /// <summary>
    /// Unsubscribes from events, to disable the ability
    /// </summary>
    public void OnDisable()
    {
        PlayerInput.Instance.OnUse.RemoveListener(ToggleAbility);
    }

    /// <summary>
    /// Is used to toggle the ability on and off
    /// It uses the input to activating the appropiate function
    /// </summary>
    /// <param name="active"></param>
    private void ToggleAbility(bool active)
    {
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
}
