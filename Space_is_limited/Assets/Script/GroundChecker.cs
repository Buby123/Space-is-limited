using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Checks if player stays on the ground
/// </summary>
public class GroundChecker : Singleton<GroundChecker>
{
    #region objects
    [Tooltip("The Tilmap that is used for the platforms")]
    [SerializeField] private Tilemap platformMap;
    #endregion

    #region variables
    public bool onGround { get; set; } = false;

    [Tooltip("Time in which the player can jump after leaving a platform")]
    [SerializeField] private float delayTime = 0.05f;
    private bool newGroundState = true;
    private bool wasInPlatform = false;
    #endregion

    #region UnityFunctions
    /// <summary>
    /// Checks if player is still in a platform
    /// </summary>
    private void Update()
    {
        if(wasInPlatform && !isInPlatform())
        {
            Collider2D collider = GetComponent<Collider2D>();
            Vector2 bounds_min =  collider.bounds.min;
            Vector3Int tilePos_min = platformMap.WorldToCell(bounds_min);

            if(platformMap.GetTile(tilePos_min) != null)
            {
                wasInPlatform = false;
                onGround = true;
                newGroundState = true;
            }
        }
    }

    /// <summary>
    /// Triggers when enters a Trigger Field / Collider -> Start of onGround = true
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            if(isInPlatform()) {
                wasInPlatform = true;
            }
            else {
                onGround = true;
                newGroundState = true;
            }
        }
        else {
            onGround = true;
            newGroundState = true;
        }
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
    #endregion


    #region OurFunctions
    /// <summary>
    /// Sets the GroundState to the delayed value
    /// </summary>
    private void ChangeGroundState(){
        onGround = newGroundState;
    }

    /// <summary>
    /// Checks if the middle part of the player is in a platform
    /// </summary>
    ///<returns>true if player is in a platform and vice versa</returns>
    private bool isInPlatform() {
        Collider2D collider = GetComponent<Collider2D>();
        Vector2 bounds_center = collider.bounds.center;
        Vector3Int tilePos_centre = platformMap.WorldToCell(bounds_center);

        if(platformMap.GetTile(tilePos_centre) != null) {
            return true;
        }
        else {
            return false;
        }
    }
    #endregion
}
