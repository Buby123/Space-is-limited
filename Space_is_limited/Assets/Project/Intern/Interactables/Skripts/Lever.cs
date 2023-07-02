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
    [Tooltip("Pushed Graphic")]
    [SerializeField] private Sprite PushedLeverGraphic;
    [Tooltip("Released Graphic")]
    [SerializeField] private Sprite ReleasedLeverGraphic;
    [Tooltip("Help Graphic for the button")]
    [SerializeField] private GameObject HelpGraphic;
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
        // PlayerInput.Instance.OnInteraction.AddListener(OnInteractionKey);                   <------------------------- Fix this failure if you want to use the Lever!!
    }

    /// <summary>
    /// Activates the possibility to change the lever state
    /// </summary>
    /// <param name="collision">Collider of player</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            HelpGraphic.SetActive(true);
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
            HelpGraphic.SetActive(false);
            canBeChanged = false;
        }
    }

    /// <summary>
    /// Test if the lever is pulled by the player and changes the state after an interaction.
    /// Is invoked by an event listener.
    /// </summary>
    private void OnInteractionKey()
    {
        if (canBeChanged)
        {
            SetState(!active);
        }
    }
    
    #endregion

    #region VisualOutput
    /// <summary>
    /// Sets the visual state of the lever to a different one
    /// </summary>
    /// <param name="state">new state of the lever</param>
    private void SetState(bool state)
    {
        active = state;

        if (state)
        {
            VisualLever.sprite = PushedLeverGraphic;
        } else
        {
            VisualLever.sprite = ReleasedLeverGraphic;
        }

        EventManager.Instance.PullLever(id, active);
    }
    #endregion
}
