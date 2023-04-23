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
    #region Variables
    private KeyCode rightKey = KeyCode.D;
    private KeyCode leftKey = KeyCode.A;
    public UnityEvent<float> OnSidewardValue { get; private set; }
    public UnityEvent<float> OnUpsideValue { get; private set; }

    private KeyCode downKey = KeyCode.S;
    public UnityEvent<bool> OnDown { get; private set; }

    private KeyCode jumpKey = KeyCode.W;
    public UnityEvent<bool> OnJump { get; private set; }

    private KeyCode interactionKey = KeyCode.E;
    public UnityEvent OnInteraction { get; private set; }

    private KeyCode specialAbilityKey = KeyCode.Space;
    public UnityEvent<bool> OnSpecialAbility { get; private set; }

    private KeyCode menuKey = KeyCode.Escape;
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

    /// <summary>
    /// Handles the Input and invokes the Events
    /// </summary>
    private void Update()
    {
        OnSidewardValue.Invoke(GetHorizontalInput());
        OnUpsideValue.Invoke(GetVerticalInput());

        MessageOnChange(OnDown, downKey);
        MessageOnChange(OnJump, jumpKey);
        MessageOnKeyDown(OnInteraction, interactionKey);
        MessageOnChange(OnSpecialAbility, specialAbilityKey);
        MessageOnKeyDown(OnMenu, menuKey);
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
    /// </summary>
    /// <returns> A Value between x and y, representing the input axis </returns>
    private float GetHorizontalInput()
    {
        float horizontalInput = 0;

        if(Input.GetKeyDown(rightKey) || Input.GetKey(rightKey))
        {
            horizontalInput = 1;
        } else if(Input.GetKeyDown(leftKey) || Input.GetKey(leftKey))
        {
            horizontalInput = -1;
        }
        return horizontalInput;
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

        if (Input.GetKeyDown(jumpKey) || Input.GetKey(jumpKey))
        {
            verticalInput = 1;
        }
        else if (Input.GetKeyDown(downKey) || Input.GetKey(downKey))
        {
            verticalInput = -1;
        }
        return verticalInput;
    }

    /// <summary>
    /// This function can be called to get the KeyCode of a specific action.
    /// </summary>
    /// <param name="actionName"> Name of the Action that should be replaced </param>
    /// <returns> Keycode of a specific Action</returns>
    public KeyCode GetKeyCodeOfAction(string actionName)
    {
        switch(actionName)
        {
            case "Move_Right":
                return rightKey;
            case "Move_Left":
                return leftKey;
            case "Move_Down":
                return downKey;
            case "Jump":
                return jumpKey;
            case "Interaction":
                return interactionKey;
            case "Special_Ability":
                return specialAbilityKey;
            case "Open/Close_Menu":
                return menuKey;
            default:
                Debug.LogError("Action Name was not registered!");
                return KeyCode.None;
        }
    }

    /// <summary>
    /// This function can be called to assign a new KeyCode to a specific action.
    /// </summary>
    /// <param name="actionName"> Name of the Action that should be replaced </param>
    /// <param name="keyCode"> The new KeyCode for that action </param>
    public void SetKeyCodeOfAction(string actionName, KeyCode keyCode)
    {
        if (!IsKeyCodeAvailable(keyCode))
        {
            Debug.LogError("Assignement of new KeyCode has failed because the Key is alread assigned otherwise!");
            return;
        }

        switch (actionName)
        {
            case "Move_Right":
                rightKey = keyCode;
                break;
            case "Move_Left":
                leftKey = keyCode;
                break;
            case "Move_Down":
                downKey = keyCode;
                break;
            case "Jump":
                jumpKey = keyCode;
                break;
            case "Interaction":
                interactionKey = keyCode;
                break;
            case "Special_Ability":
                specialAbilityKey = keyCode;
                break;
            case "Open/Close_Menu":
                menuKey = keyCode;
                break;
            default:
                Debug.LogError("Action Name was not registered!");
                break;
        }
    }

    /// <summary>
    /// This function can be used to see if a specific KeyCode is already in use.
    /// Before assigning a new KeyCode there should always be the security that
    /// there are no collisions between KeyCode assignments. This can be done using this function.
    /// </summary>
    /// <param name="keyCode"> The KeyCode that should be tested </param>
    /// <returns> Returns if the given KeyCode is not in use currently </returns>
    public bool IsKeyCodeAvailable(KeyCode keyCode)
    {
        if(keyCode == rightKey || keyCode == leftKey || keyCode == downKey || keyCode == jumpKey ||
           keyCode == interactionKey || keyCode == specialAbilityKey || keyCode == menuKey)
        {
            return false;
        }
        return true;
    }
}
