using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controles all aspects of the player
/// </summary>
public class PlayerController : Singleton<PlayerController>
{
    #region objects
    [Tooltip("The Sprite and Collider of the player")]
    [SerializeField] GameObject Appearance;
    private Rigidbody2D Controller;
    #endregion

    #region variables
    [Tooltip("Toggles the walking speed of the player")]
    [SerializeField] float speed = 0f;
    [Tooltip("Toggles the speed Speed with which the player jumps off the ground")]
    [SerializeField] float maxJumpSpeed = 0f;
    [Tooltip("Defines the speed reduction while not pressing w and jumping")]
    [SerializeField] float jumpReduction = 1f;

    bool flippedLeft = false;
    bool inJump = false;
    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private new void Awake()
    {
        Controller = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Updates the different aspects of the player
    /// The movement, the appearance and the falling through platforms of the player are handled here
    /// </summary>
    private void Update()
    {
        float inputVector = Input.GetAxis("Horizontal");
        bool isOnGround = GroundChecker.Instance.onGround;

        float ySpeed = TestForJumpStart(isOnGround, Controller.velocity.y);

        ySpeed = GetCurrentJumpVelocity(ySpeed);
        
        Controller.velocity = new Vector2(inputVector * speed, ySpeed);

        flipLikeMovement(inputVector);

        FallTroughPlatform(isOnGround);
    }
    #endregion

    #region OurFunctions
    /// <summary>
    /// when the s-key is pressed lets the player fall down through platforms
    /// </summary>
    /// <param name="isOnGround">Is the player on the ground</param>
    private void FallTroughPlatform(bool isOnGround) 
    {
        if (!isOnGround || !Input.GetKey(KeyCode.S)) 
        {
            return;
        }

        Appearance.layer = LayerMask.NameToLayer("PlayerOffPlatform");
        Invoke(nameof(resetLayer), 0.2f);
    }

    /// <summary>
    /// Tests if the player is jumping or if it is no longer jumping
    /// </summary>
    /// <param name="isOnGround">Is the player on the ground</param>
    /// <param name="currentSpeed">The actual speed of the player</param>
    /// <returns>The new speed of the player</returns>
    private float TestForJumpStart(bool isOnGround, float currentSpeed)
    {
        if (Input.GetKey(KeyCode.W) && isOnGround && !inJump)
        {
            inJump = true;
            return maxJumpSpeed;
        } 
        
        if (inJump && isOnGround && Controller.velocity.y <= maxJumpSpeed / 4)
        {
            inJump = false;
        }

        return currentSpeed;
    }

    /// <summary>
    /// Changes the look of the player to the direction he is moving
    /// </summary>
    /// <param name="inputVector">The input of the player in x direction</param>
    private void flipLikeMovement(float inputVector)
    {
        if (inputVector > 0 && flippedLeft)
        {
            flip();
        }
        else if (inputVector < 0 && !flippedLeft)
        {
            flip();
        }
    }

    /// <summary>
    /// Flips the player to other side
    /// </summary>
    private void flip()
    {
        Vector3 currentScale =  gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flippedLeft = !flippedLeft;
    }
    
    /// <summary>
    /// Resets the layer of the player so he can't fall through platforms
    /// </summary>
    private void resetLayer() {
        Appearance.layer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// Calculates the new jump speed when the player is not pressing w or is falling
    /// </summary>
    /// <param name="oldSpeed">The y speed before the new calculation</param>
    /// <returns>The new jump speed</returns>
    private float GetCurrentJumpVelocity(float oldSpeed)
    {
        if (!inJump || Input.GetKey(KeyCode.W) || Controller.velocity.y < 0)
        {
            return oldSpeed;
        }
        else
        {
            return Mathf.Max(oldSpeed - jumpReduction / Time.deltaTime, 0f);
        }
    }
    #endregion
}
