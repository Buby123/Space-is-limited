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
    private bool canBeChanged = false;
    [Tooltip("ID of the signal")]
    [SerializeField] private int id;
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
    /// Activates the possibility to change the button state
    /// </summary>
    /// <param name="collision">Collider of player</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            canBeChanged = true;
        }
    }

    /// <summary>
    /// Deactives the possibility to change the button state
    /// </summary>
    /// <param name="collision">Collider of player</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            canBeChanged = false;
        }
    }

    /// <summary>
    /// Test if the button is pulled by the player and pushes it
    /// </summary>
    private void Update()
    {
        if (canBeChanged)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Push();
            }
        }

        FloatToPosition();
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

    private void FloatToPosition()
    {
        RB.velocity = new Vector2(0, InitialPosition.y - transform.position.y);
    }
    #endregion
}
