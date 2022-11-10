using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controles the Movement of the player
/// </summary>
public class PlayerController : Singleton<PlayerController>
{
    #region objects
    private Rigidbody2D Controller;
    #endregion

    #region variables
    [Tooltip("Toggles the speed of the player")]
    [SerializeField] float speed = 0f;
    [Tooltip("Toggles the maximum of the Jump Speed")]
    [SerializeField] float maxJumpSpeed = 0f;

    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private new void Awake()
    {
        Controller = gameObject.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// movement-update
    /// </summary>
    private void FixedUpdate()
    {
        
        float inputVector = Input.GetAxis("Horizontal");
        bool isOnGround = GroundChecker.Instance.onGround;
        
        float yVelocity;

        if(Input.GetKey(KeyCode.W) && isOnGround) {
            yVelocity =  maxJumpSpeed;
        }
        else if(!Input.GetKey(KeyCode.W) && Controller.velocity.y > 0) {
            yVelocity = Controller.velocity.y*0.8f;
        }
        else {
            yVelocity = Controller.velocity.y;
        }

        Controller.velocity = new Vector2(speed * inputVector, yVelocity);

    }
    #endregion
}
