using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves the Box if the player stands above it
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class BoxWallRecoverer : MonoBehaviour
{
    private Transform _PlayerTF;
    private Rigidbody2D _RB;

    private bool _playerTouchesBox = false;
    [Tooltip("Force of footsteps from player above the box")]
    [SerializeField] private float movement_force = 10f;

    #region UnityMethods
    /// <summary>
    /// Inits the parameters
    /// </summary>
    private void Start()
    {
        _PlayerTF = GameObject.FindGameObjectWithTag("Player").transform;
        _RB = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Moves the box when player stands above
    /// </summary>
    private void Update()
    {
        if (_playerTouchesBox && PlayerAboveBox())
        {
            MoveBoxOnPlayerMovement();
        }
    }
    #endregion

    #region Player Touches Box
    /// <summary>
    /// Check if player touches the box
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerTouchesBox = true;
        }
    }

    /// <summary>
    /// Check if player touches the box
    /// </summary>
    /// <param name="other"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            _playerTouchesBox = false;
        }
    }
    #endregion

    #region Movebox back on movement
    /// <summary>
    /// Test if the player is higher than the box
    /// </summary>
    /// <returns></returns>
    private bool PlayerAboveBox()
    {
        var BoxPosition = transform.position;

        return _PlayerTF.position.y > BoxPosition.y;
    }

    /// <summary>
    /// Move box in opposite direction of player movement
    /// </summary>
    private void MoveBoxOnPlayerMovement()
    {
        var VerticalVector = Input.GetAxis("Horizontal");
        _RB.AddForce(new Vector2(movement_force * -VerticalVector, 0));
    }
    #endregion
}
