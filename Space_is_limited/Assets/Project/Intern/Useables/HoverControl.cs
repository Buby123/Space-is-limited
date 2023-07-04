using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

/// <summary>
/// Moves an object with the input of the player, (!must not be active at the beginning!)
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class HoverControl : MonoBehaviour
{
    [SerializeField] private float _MaxSpeed = 30f;
    [SerializeField] private float _MaxAcceleration = 80f;

    private Vector2 _Velocity = new Vector2(0f, 0f);

    private Rigidbody2D _RB;
    private Rigidbody2D RB
    {
        get
        {
            _RB = _RB != null ? _RB : GetComponent<Rigidbody2D>();
            return _RB;
        }
    }

    /// <summary>
    /// Activates the movement when the component is activated
    /// </summary>
    public void OnEnable()
    {
        PlayerInput.Instance.OnSidewardValue.AddListener(MoveSideward);
        PlayerInput.Instance.OnUpsideValue.AddListener(MoveUpside);
        RB.gravityScale = 0;
    }

    /// <summary>
    /// Deactivates the movement when the component is deactivated
    /// </summary>
    public void OnDisable()
    {
        PlayerInput.Instance.OnSidewardValue.RemoveListener(MoveSideward);
        PlayerInput.Instance.OnUpsideValue.RemoveListener(MoveUpside);
        RB.gravityScale = 2.8f;
    }

    /// <summary>
    /// Moves the object sideways
    /// by setting the speed for the FixedUpdate
    /// </summary>
    /// <param name="value">Change in a direction</param>
    public void MoveSideward(float value)
    {
        _Velocity.x = value * _MaxSpeed;
    }

    /// <summary>
    /// Moves the object up or down
    /// By setting the speed for the FixedUpdate
    /// </summary>
    /// <param name="value">Change in height</param>
    public void MoveUpside(float value)
    {
        _Velocity.y = value * _MaxSpeed;
    }

    private void Update()
    {
        // Energy Costs
    }

    /// <summary>
    /// Accelerates the object to the desired speed with the desired acceleration.
    /// 
    /// If the speed falls below a threshold of 0.02, it is zeroed.
    /// This fixes any wobbling without input.
    /// </summary>
    private void FixedUpdate()
    {
        if (Mathf.Abs(RB.velocity.x) > 0.02 || Mathf.Abs(RB.velocity.y) > 0.02 || _Velocity.y != 0 || _Velocity.x != 0)
        {
            RB.AddForce(HelpFunctions.GetAccelerationVelocity(_Velocity, RB.velocity, _MaxAcceleration), ForceMode2D.Impulse);
        }

        if(Mathf.Abs(RB.velocity.x) <= 0.02)
        {
            RB.velocity = new Vector2(0, RB.velocity.y);
        }
        if(Mathf.Abs(RB.velocity.y) <= 0.02)
        {
            RB.velocity = new Vector2(RB.velocity.x, 0);
        }
    }
}
