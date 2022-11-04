using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player stays on the ground
/// </summary>
public class GroundChecker : Singleton<GroundChecker>
{
    public bool onGround { get; set; } = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
    }
}
