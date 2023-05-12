using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class AbilityStateControl : Singleton<AbilityStateControl>
{
    Dictionary<AbilityType, PlayerAbility> PlayerAbilities = new();

    [SerializeField] private LayerMask ObjectLayer;
        
    private AbilityType activeAbility;

    // Start is called before the first frame update
    void Start()
    {
        SetupAbilities();
        SetActive(AbilityType.Hoverer);
    }

    /// <summary>
    /// Includes every ability the player has
    /// by first giving an enum type and then giving an ability class
    /// </summary>
    private void SetupAbilities()
    {
        PlayerAbilities.Add(AbilityType.Hoverer, new Hoverer(ObjectLayer));
    }

    /// <summary>
    /// Sets an Ability active by giving an enum type
    /// </summary>
    /// <param name="Type">Type of the ability</param>
    private void SetActive(AbilityType Type)
    {
        DeactivateAbility();

        if (Type == AbilityType.None)
        {
            return;
        }

        activeAbility = Type;
        PlayerAbilities[activeAbility].OnEnable();
    }

    /// <summary>
    /// Deactivates the current ability
    /// By calling on disable
    /// </summary>
    private void DeactivateAbility()
    {
        if (activeAbility == AbilityType.None)
        {
            return;
        }

        PlayerAbilities[activeAbility].OnDisable();
        activeAbility = AbilityType.None;
    }

    /// <summary>
    /// Gives an update to the ability
    /// for specific abilitys
    /// </summary>
    public void Update()
    {
        if (activeAbility == AbilityType.None)
        {
            return;
        }

        PlayerAbilities[activeAbility].Update();
    }
}

public enum AbilityType
{
    Hoverer,
    None
}
