using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the Keys used for the player, with UnityEvents
/// When a key is pressed, the event is invoked
/// </summary>
public class PlayerInput : Singleton<PlayerInput>
{
    [SerializeField] private KeyCode jumpKey = KeyCode.W;
    public UnityEvent<bool> OnJump { get; private set; }

    [SerializeField] private KeyCode interactKey = KeyCode.E;
    public UnityEvent OnInteract { get; private set; }

    [SerializeField] private KeyCode fallthroughKey = KeyCode.S;
    public UnityEvent<bool> OnFalltrough { get; private set; }

    public UnityEvent<float> OnSidewardValue { get; private set; }
    
    /// <summary>
    /// Prepares the events
    /// </summary>
    private void OnEnable()
    {
        OnJump ??= new();
        OnInteract ??= new();
        OnFalltrough ??= new();
        OnSidewardValue ??= new();
    }

    /// <summary>
    /// Handles the Input and invokes the Events
    /// </summary>
    private void Update()
    {
        // Input Values
        OnSidewardValue.Invoke(Input.GetAxis("Horizontal"));

        // Messages on Key Down
        MessageOnChange(OnJump, jumpKey);
        MessageOnKeyDown(OnInteract, interactKey);
        MessageOnChange(OnFalltrough, fallthroughKey);
    }

    /// <summary>
    /// Invokes the Event when the Key is pressed or released
    /// </summary>
    /// <param name="Event">Event that should be triggered</param>
    /// <param name="keyCode">Key that indicades the event</param>
    private void MessageOnChange(UnityEvent<bool> Event, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Event.Invoke(true);
        } else if (Input.GetKeyUp(keyCode))
        {
            Event.Invoke(false);
        }
    }

    /// <summary>
    /// Invokes the Event when the Key is pressed
    /// </summary>
    /// <param name="Event">Event that should be triggered</param>
    /// <param name="keyCode">Key that indicades the event</param>
    private void MessageOnKeyDown(UnityEvent Event, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            Event.Invoke();
        }
    }
}
