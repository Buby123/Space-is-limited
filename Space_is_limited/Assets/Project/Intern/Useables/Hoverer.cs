using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

/// <summary>
/// Activates the control of objects and deactivates the control over the player, (!must not be activated at the beginning!)
/// </summary>
[System.Serializable]
public class Hoverer : PlayerAbility
{
    [SerializeField] private LayerMask _LayerMask;
    
    private HoverControl target;
    private float range = 10f;

    public Hoverer(LayerMask Layer)
    {
        _LayerMask = Layer;
    }

    /// <summary>
    /// Activates the control of objects and deactivates the control over the player, if an object is in front of the player
    /// Is called when the ability is activated
    /// </summary>
    protected override void OnActivateAbility()
    {
        Debug.Log("Hoverer activated");
    }

    /// <summary>
    /// Deactivates the control of objects and activates the control over the player
    /// Is called when the ability is deactivated
    /// </summary>
    protected override void OnDeactivateAbility()
    {
        DeactivateHover();
        PlayerController.Instance.Active = true;
    }

    /// <summary>
    /// Search for an object in front of the player and activate it
    /// </summary>
    protected override void OnUpdateAbility()
    {
        ShootLaser();
    }

    /// <summary>
    /// Is used to find an object in front of the player and activate it
    /// Searches for the control of the object and activates it
    /// </summary>
    private void ShootLaser()
    {
        var Hit = LaserSelector.Instance.GetObjectInFront(range, _LayerMask);

        if (Hit == null)
        {
            return;
        }

        var Target = Hit.GetComponent<HoverControl>();

        if (Target != null)
        {
            LaserSelector.Instance.BeginConnection(Target.transform, _LayerMask, range, DeactivateHover);
            PlayerController.Instance.Active = false;
            target = Target;
            target.enabled = true;
        }
    }
    
    /// <summary>
    /// Deactivates the target and the laser
    /// </summary>
    private void DeactivateHover()
    {
        LaserSelector.Instance.EndConnection();

        if (target == null)
            return;

        target.enabled = false;
        target = null;
    }
}
