using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Steuert die Bewegung des Spielers
/// </summary>
public class PlayerController : Singleton<PlayerController>
{
    #region objects
    private Rigidbody2D Controller;
    [SerializeField] LayerMask GroundLayer;
    #endregion

    #region variables
    [SerializeField] float speed = 0f;
    [SerializeField] float jumpSpeed = 0f;
    [SerializeField] float playerHeight = 1f;
    [SerializeField] float maxVelocity = 100f;
    [SerializeField] float maxJumpVelocity = 100f;
    [SerializeField] float downVelocity = -10f;
    #endregion

    #region UnityFunctions
    /// <summary>
    /// Initialisieren des Rigidbodys
    /// </summary>
    private void Awake()
    {
        Controller = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Bewegungsupdates
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 velocity = Controller.velocity;
        float inputVector = Input.GetAxis("Horizontal");
        float jumpVector = Input.GetAxis("Vertical");
        bool jump = false;

        //Sprunggeschwindigkeit
        if (jumpVector > 0)
        {
            if (GroundChecker.Instance.onGround)
            {
                jump = true;
            }
        }

        //Geschwindigkeit cappen nach links rechts
        Controller.velocity = new Vector2(speed * inputVector,
                             jump ? jumpSpeed : Mathf.Min(Controller.velocity.y, Mathf.Max(maxJumpVelocity * (jumpVector + 0.5f), downVelocity)));
    }
    #endregion
}
