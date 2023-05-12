using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Aktiviert die Kontrolle von Objekten und deaktiviert die Kontrolle über den Spieler, (!darf nicht zu beginn aktiviert sein!)
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
    /// Aktiviert die Kontrolle von Objekten und deaktiviert die Kontrolle über den Spieler, falls ein Objekt vor dem Spieler ist
    /// Wird aufgerufen sobald die Fähigkeit aktiviert wird
    /// </summary>
    protected override void OnActivateAbility()
    {
        Debug.Log("Hoverer activated");
    }

    /// <summary>
    /// Deaktiviert die Kontrolle von Objekten und aktiviert die Kontrolle über den Spieler
    /// Wird aufgerufen sobald die Fähigkeit deaktiviert wird
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
    /// Wird benutzt um ein Objekt vor dem Spieler zu finden und es zu aktivieren
    /// Sucht dafür die Kontrolle des Objekts und aktiviert diese
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
    /// Deaktiviert das Ziel und deaktiviert dieses
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
