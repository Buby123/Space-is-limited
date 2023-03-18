using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;


/// <summary>
/// controles the Movement of the player
/// </summary>
[RequireComponent(typeof(PlayerInput))]
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
    [Tooltip("Toggles the look direction of the player")]
    [SerializeField] bool flippedLeft = true;
    [SerializeField] GameObject Appearance;

    #endregion

    #region Propertys
    [field: SerializeField] public Collider2D PlayerCollider { get; private set; }
    #endregion

    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private new void Awake()
    {
        Controller = GetComponent<Rigidbody2D>();
    }

    #region Input Functions
    public void OnMove(InputAction.CallbackContext context)
    {
        var horizontalMovement = context.ReadValue<float>();

        //Set player velocity in x direction
        Controller.velocity = new Vector2(speed * horizontalMovement, Controller.velocity.y);
        
        //Change the look of the player (inclusive colliders)
        if (horizontalMovement > 0 && flippedLeft)
        {
            flip();
        }
        else if (horizontalMovement < 0 && !flippedLeft)
        {
            flip();
        }
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        var shiftButton = context.performed;

        //when the s-key is pressed lets the player fall down through platforms
        if (shiftButton)
        {
            Appearance.layer = LayerMask.NameToLayer("PlayerOffPlatform");
        }
        else
        {
            Appearance.layer = LayerMask.NameToLayer("Player");
        }
    }

    public void OnJump()
    {
        if (!GroundChecker.Instance.onGround)
            return;
                
        Controller.velocity = new Vector2(Controller.velocity.x, maxJumpSpeed);
    }

    public void OnReduceJumpSpeed(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5)
            return;

        Debug.Log("Reduce jump speed");

        float ySpeed = Controller.velocity.y;

        if (ySpeed <= 0f)
            return;

        Controller.velocity = new Vector2(Controller.velocity.x, ySpeed * 0.5f);
    }
    #endregion

    #region Other Functions
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
    #endregion
}