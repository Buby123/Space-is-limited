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
    [Tooltip("Jumpforce")]
    [SerializeField] float jumpForce = 20f;
    [Tooltip("Toggles the force to move")]
    [SerializeField] float sideForce = 10f;
    [Tooltip("Toggles the maximum speed the player can reach")]
    [SerializeField] float maxSideSpeed = 7f;
    [Tooltip("Toggles the look direction of the player")]
    [SerializeField] bool flippedLeft = true;
    [SerializeField] GameObject Appearance;

    private bool jump;
    private float moveSidewardsInput;

    #endregion

    #region Propertys
    [field: SerializeField] public Collider2D PlayerCollider { get; private set; }
    #endregion

    #region UnityFunctions
    /// <summary>
    /// initializes the Rigidbodys
    /// </summary>
    private void Start()
    {
        Controller = GetComponent<Rigidbody2D>();
        PlayerInput.Instance.OnJump.AddListener(OnJump);
        PlayerInput.Instance.OnFalltrough.AddListener(OnFalltrough);
        PlayerInput.Instance.OnSidewardValue.AddListener(OnSidewardValue);
    }
    #endregion

    #region Input Functions
    /// <summary>
    /// Jump or fall faster
    /// </summary>
    public void OnJump(bool jump)
    {
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
        this.moveSidewardsInput = moveSidewardsInput;
        
        //Change the look of the player (inclusive colliders)
        if (moveSidewardsInput > 0 && flippedLeft)
        {
            flip();
        }
        else if (moveSidewardsInput < 0 && !flippedLeft)
        {
            flip();
        }
    }

    /// <summary>
    /// Lets the player fall through platforms by changing its 
    /// </summary>
    public void OnFalltrough(bool pushed)
    {
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
        // Move sidewards
        AccelerateTo(moveSidewardsInput * sideForce, Controller.velocity.x, Vector2.right);

        // Fall fast
        if (!jump && Controller.velocity.y > 0)
        {
            Controller.velocity = new Vector2(Controller.velocity.x, Controller.velocity.y * 0.8f);
        }
    }

    /// <summary>
    /// Accelerates to a specific speed
    /// </summary>
    /// <param name="targetVelocity"></param>
    /// <param name="currentVelocity"></param>
    private void AccelerateTo(float targetVelocity, float currentVelocity, Vector3 direction)
    {
        var deltaV = targetVelocity - currentVelocity;
        var accel = deltaV / Time.deltaTime;

        accel = Mathf.Clamp(accel, -maxSideSpeed, maxSideSpeed);
        Controller.AddForce(direction * accel, ForceMode2D.Force);
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
    #endregion
}