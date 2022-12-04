using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Checks if player is on the ground
/// </summary>
public class GroundChecker : Singleton<GroundChecker>
{
    #region objects
    [SerializeField] private LayerMask GroundLayer;
    [SerializeField] private LayerMask PlatformLayer;
    [Tooltip("The Collider of the player")]
    [SerializeField] private Collider2D Coll;
    [SerializeField] private Rigidbody2D Controller;
    #endregion

    #region variables
    public bool onGround { get; set; } = false;
    [Tooltip("Time in which the player can jump after leaving a platform")]
    [SerializeField] private float delayTime = 0.05f;
    private bool newGroundState = true;
    #endregion
    
    #region UnityFunctions
    /// <summary>
    /// Updates the ground state
    /// </summary>
    private void Update()
    {
        if(isOnGround()) {
            onGround = true;
            newGroundState = true;
        }
        else if(newGroundState) {
            newGroundState = false;
            Invoke(nameof(ChangeGroundState), delayTime);
        }
    }
    #endregion


    #region OurFunctions
    /// <summary>
    /// Checks if the player is currently on the ground
    /// </summary>
    private bool isOnGround()
    {
        if(Controller.velocity.y < 0.01 && standsOnLayer(PlatformLayer)) {
            return true;
        }
        if(standsOnLayer(GroundLayer)) {
            return true;
        }
        return false;

    }

    /// <summary>
    /// Checks if the player stands on a layer
    /// </summary>
    ///<param name="layer"> layer to check button</param>
    private bool standsOnLayer(LayerMask layer)
    {
        return Physics2D.BoxCast(Coll.bounds.center, Coll.bounds.size, 0f, Vector2.down,  0.1f, layer) &&
        !Physics2D.BoxCast(Coll.bounds.center, Coll.bounds.size, 0f, Vector2.zero, 0.1f, layer);
    }

    /// <summary>
    /// Sets the GroundState to the delayed value
    /// </summary>
    private void ChangeGroundState(){
        onGround = newGroundState;
    }
    #endregion
}
