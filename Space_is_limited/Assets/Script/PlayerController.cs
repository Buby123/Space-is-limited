using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controles the Movement of the player
/// </summary>
public class PlayerController : Singleton<PlayerController>
{
    #region objects
    [Tooltip("The Sprite and Collider of the player")]
    [SerializeField] GameObject Appearance;
    private Rigidbody2D Controller;
    #endregion

    #region variables
    [Tooltip("Toggles the speed of the player")]
    [SerializeField] float speed = 0f;
    [Tooltip("Toggles the maximum of the Jump Speed")]
    [SerializeField] float maxJumpSpeed = 0f;
    [Tooltip("Defines the speed reduction while not pressing w and jumping")]
    [SerializeField] float jumpReduction = 1f;

    bool flippedLeft = false;
    bool inJump = false;
    float timeSinceJump = 0f;
    float reductionJumpTime = 0f;

    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private new void Awake()
    {
        Controller = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float inputVector = Input.GetAxis("Horizontal");
        bool isOnGround = GroundChecker.Instance.onGround;

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
    }
    #endregion

    #region OurFunctions
    /// <summary>
    /// Flips the Player per localScale parameter
    /// </summary>
    private void flip()
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
    #endregion
}
