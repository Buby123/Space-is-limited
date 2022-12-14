using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Lever handles a interaction and send signals to other objects
/// </summary>
public class Lever : MonoBehaviour
{
    #region Objects
    [Tooltip("Visual Output of the Lever (Renderer)")]
    [SerializeField] private SpriteRenderer VisualLever;
    [Tooltip("Color when the Lever is set to active")]
    [SerializeField] private Color ActiveColor;
    [Tooltip("Color when the Lever is set to inactive")]
    [SerializeField] private Color DeactiveColor;
    #endregion

    #region Variables
    private bool active = false;
    private bool canBeChanged = false;
    [Tooltip("ID of the signal")]
    [SerializeField] private int id;
    #endregion

    #region Triggers
    /// <summary>
    /// Set the startstate
    /// </summary>
    private void Awake()
    {
        SetState(active);
    }

    /// <summary>
    /// Activates the possibility to change the lever state
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
    /// Deactives the possibility to change the lever state
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
    /// Test if the lever is pulled by the player and changes the state
    /// </summary>
    private void Update()
    {
        if (canBeChanged)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SetState(!active);
            }
        }
    }
    #endregion

    #region VisualOutput
    /// <summary>
    /// Sets the visual state of the lever to another one
    /// </summary>
    /// <param name="state">new state of the lever</param>
    private void SetState(bool state)
    {
        active = state;

        if (state)
        {
            VisualLever.color = ActiveColor;
        } else
        {
            VisualLever.color = DeactiveColor;
        }

        EventManager.Instance.PullLever(id, active);
    }
    #endregion
}
