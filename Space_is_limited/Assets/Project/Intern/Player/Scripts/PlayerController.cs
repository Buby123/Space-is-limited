using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controles the Movement of the player
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Singleton<PlayerController>
{
    #region objects
    private Rigidbody2D Controller;
    #endregion

    #region variables
    [Tooltip("Jumpforce")]
    [SerializeField] float jumpForce = 20f;
    [Tooltip("Toggles the force to move")]
    [SerializeField] float sideSpeed = 10f;
    [Tooltip("Toggles the maximum speed the player can reach")]
    [SerializeField] float maxSideAcceleration = 7f;
    [Tooltip("Toggles the look direction of the player")]
    [SerializeField] bool flippedLeft = true;
    [SerializeField] GameObject Appearance;

    private bool jump;
    private float moveSidewardsInput;
    #endregion

    #region Propertys
    [field: SerializeField] public Collider2D PlayerCollider { get; private set; }
    public bool Active { get; set; } = true;
    public bool FlippedLeft => flippedLeft;
    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private void Start()
    {
        Controller = GetComponent<Rigidbody2D>();
        PlayerInput.Instance.OnJump.AddListener(OnJump);
        PlayerInput.Instance.OnDown.AddListener(OnFalltrough);
        PlayerInput.Instance.OnSidewardValue.AddListener(OnSidewardValue);
    }
    #endregion

    #region Input Functions
    /// <summary>
    /// Jump or fall faster
    /// </summary>
    public void OnJump(bool jump)
    {
        if (!Active)
        {
            return;
        }

        this.jump = jump;
        
        if (jump && GroundChecker.Instance.onGround)
        {
            Controller.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Moves the player sideways and flips the image if needed
    /// </summary>
    /// <param name="moveSidewardsInput">float from -1 to 1, that characterizes left to right</param>
    public void OnSidewardValue(float moveSidewardsInput)
    {
        if (!Active)
        {
            return;
        }

        this.moveSidewardsInput = moveSidewardsInput;
        
        //Change the look of the player (inclusive colliders)
        if (moveSidewardsInput > 0 && flippedLeft)
        {
            Flip();
        }
        else if (moveSidewardsInput < 0 && !flippedLeft)
        {
            Flip();
        }
    }

    /// <summary>
    /// Lets the player fall through platforms by changing its 
    /// </summary>
    public void OnFalltrough(bool pushed)
    {
        if (!Active)
        {
            return;
        }

        if (pushed)
        {
            Appearance.layer = LayerMask.NameToLayer("PlayerOffPlatform");
        } else
        {
            Appearance.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void FixedUpdate()
    {
        if (!Active)
        {
            return;
        }

        // Move sidewards
        Controller.AddForce(Vector2.right * HelpFunctions.GetAccelerationVelocity(moveSidewardsInput * sideSpeed, Controller.velocity.x, maxSideAcceleration), ForceMode2D.Force);

        // Fall fast
        if (!jump && Controller.velocity.y > 0)
        {
            Controller.velocity = new Vector2(Controller.velocity.x, Controller.velocity.y * 0.8f);
        }
    }
    #endregion

    #region OurFunctions
    /// <summary>
    /// Flips the Player per localScale parameter
    /// </summary>
    private void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        flippedLeft = !flippedLeft;
    }
    #endregion
}