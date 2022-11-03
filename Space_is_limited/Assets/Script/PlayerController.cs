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
        bool jump = false;

        //Sprunggeschwindigkeit
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("TryJump");
            if (IsGrounded())
            {
                jump = true;
                Debug.Log("Jump");
            }
        }

        //Bewegung Rechts Links
        Controller.AddForce(new Vector2(inputVector * speed, jump ? jumpSpeed : 0f) * Time.deltaTime);

        //Geschwindigkeit cappen nach links rechts
        Controller.velocity = new Vector2(Mathf.Clamp(Controller.velocity.x, -maxVelocity, maxVelocity),
                              Controller.velocity.y);
    }
    #endregion

    #region testFunctions
    /// <summary>
    /// Gibt aus ob der Spieler playerHeight vom Boden entfernt ist
    /// </summary>
    /// <returns>Ist playerHeight vom Boden entfernt oder weniger</returns>
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, playerHeight, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
    #endregion
}
