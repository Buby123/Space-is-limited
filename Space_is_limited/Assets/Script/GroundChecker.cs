using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks if player stays on the ground
/// </summary>
public class GroundChecker : Singleton<GroundChecker>
{
    public bool onGround { get; set; } = true;
    
    [SerializeField] private float delayTime = 0.05f;
    private bool newGroundState = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onGround = true;
        newGroundState = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        newGroundState = false;
        Invoke("ChangeGroundState", delayTime);
    }

    private void ChangeGroundState(){
        onGround = newGroundState;
    }
}
