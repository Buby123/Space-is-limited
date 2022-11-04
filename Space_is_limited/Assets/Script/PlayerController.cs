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
    #endregion

    #region variables
    [SerializeField] float speed = 0f;
    [SerializeField] float maxJumpSpeed = 0f;
    [SerializeField] float jumpSpeed = 0f;

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
        float inputVector = Input.GetAxis("Horizontal");
        float jumpVector = Input.GetAxis("Vertical");
        bool isOnGround = GroundChecker.Instance.onGround;

        if(Input.GetKey(KeyCode.W) && isOnGround) {
            Controller.velocity = new Vector2(Controller.velocity.x, 0);
            jumpSpeed =  maxJumpSpeed;
            Debug.Log("maxSpeed");
        }
        else if(!Input.GetKey(KeyCode.W)) {
            jumpSpeed = 0;
            Debug.Log("reset Speed");
        }
        else if(!isOnGround) {
            jumpSpeed = 0.8f * jumpSpeed;
            Debug.Log("jump halfed");
        }

        float yVelocity = Controller.velocity.y + jumpSpeed;

        Controller.velocity = new Vector2(speed * inputVector, yVelocity);

    }
    #endregion
}
