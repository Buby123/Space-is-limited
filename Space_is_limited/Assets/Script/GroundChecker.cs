using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player stays on the ground
/// </summary>
public class GroundChecker : Singleton<GroundChecker>
{
    public bool onGround { get; set; } = true;

    [Tooltip("Time in which the player can jump after leaving a platform")]
    [SerializeField] private float delayTime = 0.05f;
    private bool newGroundState = true;

    /// <summary>
    /// Triggers when enters a Trigger Field / Collider -> Start of onGround = true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        newGroundState = true;
    }

    /// <summary>
    /// Triggers when exit a Trigger Field / Collider -> Delayed end of onGround = true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        newGroundState = false;
        Invoke("ChangeGroundState", delayTime);
    }

    /// <summary>
    /// Sets the GroundState to the delayed value
    /// </summary>
    private void ChangeGroundState(){
        onGround = newGroundState;
    }
}
