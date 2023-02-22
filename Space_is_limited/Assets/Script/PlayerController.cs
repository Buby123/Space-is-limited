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
<<<<<<< HEAD

    bool flippedLeft = false;
    bool inJump = false;
    float timeSinceJump = 0f;
    float reductionJumpTime = 0f;

=======
    [Tooltip("Difenes the time at the highest piek of the jump")]
    [SerializeField] float piekTime = 0.1f;
    [Tooltip("Is the range which counted as piek of the jump in velocity")]
    [SerializeField] float piekRange = 0.5f;

    bool flippedLeft = false;
    bool inJump = false;
    float gravityScale;
    bool inMomentZero = false;
>>>>>>> 9cd92b3ebd6500f1dcd5c741a8e1be9d4cae8720
    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private new void Awake()
    {
        Controller = GetComponent<Rigidbody2D>();
    }

<<<<<<< HEAD
=======
    /// <summary>
    /// Updates the different aspects of the player
    /// The movement, the appearance and the falling through platforms of the player are handled here
    /// </summary>
>>>>>>> 9cd92b3ebd6500f1dcd5c741a8e1be9d4cae8720
    private void Update()
    {
        float inputVector = Input.GetAxis("Horizontal");
        bool isOnGround = GroundChecker.Instance.onGround;
<<<<<<< HEAD

        float ySpeed = Controller.velocity.y;

        //Tests if the player is jumping or if it is no longer jumping
        if (Input.GetKey(KeyCode.W) && isOnGround)
        {
            //Player beginns jump
            timeSinceJump = 0f;
            reductionJumpTime = 0f;
            inJump = true;
        }
        else if (inJump && isOnGround)
        {
            //Player is back on ground
            inJump = false;
        }

        //Calculates the actual jump speed
        if (inJump)
        {
            if (!Input.GetKey(KeyCode.W))
            {
                reductionJumpTime += Time.deltaTime;
            }

            timeSinceJump += Time.deltaTime;
            ySpeed = maxJumpSpeed * Mathf.Exp(-jumpReduction * reductionJumpTime) - 9.81f * 4 * timeSinceJump;
        }

        Controller.velocity = new Vector2(inputVector * speed, ySpeed);

        //Change the look of the player (inclusive colliders)
        if (inputVector > 0 && flippedLeft)
        {
            flip();
        }
        else if (inputVector < 0 && !flippedLeft)
        {
            flip();
        }

        //when the s-key is pressed lets the player fall down through platforms
        if (Input.GetKey(KeyCode.S) && isOnGround)
        {
            Appearance.layer = LayerMask.NameToLayer("PlayerOffPlatform");
            Invoke(nameof(resetLayer), 0.2f);
        }
=======

        float ySpeed = TestForJumpStart(isOnGround, Controller.velocity.y);

        ySpeed = GetCurrentJumpVelocity(ySpeed);
        
        Controller.velocity = new Vector2(inputVector * speed, ySpeed);

        FlipLikeMovement(inputVector);

        FallTroughPlatform(isOnGround);
>>>>>>> 9cd92b3ebd6500f1dcd5c741a8e1be9d4cae8720
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
    private void FlipLikeMovement(float inputVector)
    {
        if (inputVector > 0 && flippedLeft)
        {
            Flip();
        }
        else if (inputVector < 0 && !flippedLeft)
        {
            Flip();
        }
    }

    /// <summary>
    /// Flips the player to other side
    /// </summary>
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flippedLeft = !flippedLeft;
    }

    /// <summary>
    /// Resets the layer of the player so he can't fall through platforms
    /// </summary>
    private void resetLayer()
    {
        Appearance.layer = LayerMask.NameToLayer("Player");
    }

    /// <summary>
    /// Calculates the new jump speed when the player is not pressing w or is falling
    /// </summary>
    /// <param name="oldSpeed">The y speed before the new calculation</param>
    /// <returns>The new jump speed</returns>
    private float GetCurrentJumpVelocity(float oldSpeed)
    {
        if (!inJump)
        {
            return oldSpeed;
        }

        if (!Input.GetKey(KeyCode.W) && oldSpeed > 0)
        {
            return Mathf.Max(oldSpeed - jumpReduction / Time.deltaTime, 0f);
        }
        if (Mathf.Abs(oldSpeed) < piekRange)
        {
            if (inMomentZero != true)
            {
                Debug.Log("Moment zero");
                Invoke(nameof(EndMomentOfZeroVelocity), piekTime);
                gravityScale = Controller.gravityScale;
                Controller.gravityScale = 0;
                inMomentZero = true;
            }

            return 0f;
        }
        else
        {
            inMomentZero = false;
            return oldSpeed;
        } 
    }

    private void EndMomentOfZeroVelocity()
    {
        Controller.gravityScale = gravityScale;
    }
    #endregion
}
