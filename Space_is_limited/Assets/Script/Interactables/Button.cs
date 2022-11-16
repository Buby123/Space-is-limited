using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The button handles interactions to doors and other objects
/// </summary>
public class Button : MonoBehaviour
{
    #region Objects
    [Tooltip("Visual Output of the Button (Renderer)")]
    [SerializeField] private SpriteRenderer VisualButton;
    [Tooltip("Color when the Button is set to active")]
    [SerializeField] private Color ActiveColor;
    [Tooltip("Color when the Button is set to inactive")]
    [SerializeField] private Color DeactiveColor;
    
    private Vector3 InitialPosition;
    private Rigidbody2D RB;
    #endregion

    #region Variables
    [Tooltip("ID of the signal")]
    [SerializeField] private int id;
    [Tooltip("Speed with which the button flys back in position")]
    private float floatBackSpeed = 3f;
    [Tooltip("Position distance at which the button counts as pushed")]
    private float deltaPos = 0.1f;
    #endregion

    /// <summary>
    /// Sets the output to not pushed
    /// </summary>
    private void Awake()
    {
        Reset();
        InitialPosition = transform.position;
        RB = GetComponent<Rigidbody2D>();
    }

    #region Triggers

    /// <summary>
    /// Test if the button is pulled by the player and pushes it
    /// </summary>
    private void Update()
    {
        if (FloatToPosition())
        {
            Push();
        } else
        {
            Reset();
        }
    }
    #endregion

    #region VisualOutput
    /// <summary>
    /// Sets the visual state of the button to active
    /// </summary>
    private void Push()
    {
        VisualButton.color = ActiveColor;
        EventManager.Instance.PushButton(id);
    }

    /// <summary>
    /// Sets the visual state of the button to inactive
    /// </summary>
    private void Reset()
    {
        VisualButton.color = DeactiveColor;
    }

    /// <summary>
    /// Floats back to the position, where the button was set
    /// </summary>
    /// <returns>Returns true if button is more the deltaPos away from start</returns>
    private bool FloatToPosition()
    {
        var distance = (InitialPosition.y - transform.position.y);

        //Float Back
        RB.velocity = new Vector2(0, distance * floatBackSpeed);

        return deltaPos < distance;
    }
    #endregion
}
