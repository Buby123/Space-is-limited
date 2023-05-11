using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the Keys used for the player, with UnityEvents
/// When a key is pressed, the event is invoked
/// 
/// Also provides utility functions for changing the KeyCode's of the actions
/// </summary>
public class PlayerInput : Singleton<PlayerInput>
{
    public readonly DataControls Data = new DataControls();
    
    #region Variables
    public UnityEvent<float> OnSidewardValue { get; private set; }
    public UnityEvent<float> OnUpsideValue { get; private set; }
    public UnityEvent<bool> OnDown { get; private set; }
    public UnityEvent<bool> OnJump { get; private set; }
    public UnityEvent OnInteraction { get; private set; }
    public UnityEvent<bool> OnSpecialAbility { get; private set; }
    public UnityEvent OnMenu { get; private set; }
    #endregion

    /// <summary>
    /// Prepares the events
    /// </summary>
    private void OnEnable()
    {
        OnSidewardValue ??= new();
        OnUpsideValue ??= new();
        OnDown ??= new();
        OnJump ??= new();
        OnInteraction ??= new();
        OnSpecialAbility ??= new();
        OnMenu ??= new();
    }

    private void Awake()
    {
        Data.LoadData();
    }

    /// <summary>
    /// Handles the Input and invokes the Events
    /// </summary>
    private void Update()
    {
        OnSidewardValue.Invoke(GetHorizontalInput());
        OnUpsideValue.Invoke(GetVerticalInput());

        MessageOnChange(OnDown, Data.DownKey);
        MessageOnChange(OnJump, Data.JumpKey);
        MessageOnKeyDown(OnInteraction, Data.InteractionKey);
        MessageOnChange(OnSpecialAbility, Data.SpecialAbilityKey);
        MessageOnKeyDown(OnMenu, Data.MenuKey);
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

    /// <summary>
    /// Calculates the horizontal input of the player.
    /// The function is based on Unity's -GetAxis("Horizontal")-
    /// This is dependend on the user selected 'Right' and 'Left' Keys.
    /// 
    /// If the user pressed both left & right keys, 0 is returned.
    /// </summary>
    /// <returns> A Value between x and y, representing the input axis </returns>
    private float GetHorizontalInput()
    {
        bool rightIsPressed = Input.GetKeyDown(Data.RightKey) || Input.GetKey(Data.RightKey);
        bool leftIsPressed = Input.GetKeyDown(Data.LeftKey) || Input.GetKey(Data.LeftKey);
        
        if(rightIsPressed & leftIsPressed)
        {
            return 0f;
        }
        else if (rightIsPressed)
        {
            return 1f;
        } else if(leftIsPressed)
        {
            return -1f;
        }
        return 0f;
    }

    /// <summary>
    /// Calculates the Vertical input of the player.
    /// The function is based on Unity's -GetAxis("Vertical")-
    /// This is dependend on the user selected 'Jump' and 'Down' Keys.
    /// </summary>
    /// <returns> A Value between x and y, representing the input axis </returns>
    private float GetVerticalInput()
    {
        float verticalInput = 0;

        if (Input.GetKeyDown(Data.JumpKey) || Input.GetKey(Data.JumpKey))
        {
            verticalInput = 1;
        }
        else if (Input.GetKeyDown(Data.DownKey) || Input.GetKey(Data.DownKey))
        {
            verticalInput = -1;
        }
        return verticalInput;
    }
}
