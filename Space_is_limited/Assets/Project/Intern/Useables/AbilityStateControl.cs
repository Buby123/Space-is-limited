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

    private void SetupAbilities()
    {
        PlayerAbilities.Add(AbilityType.Hoverer, new Hoverer(ObjectLayer));
    }

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

    private void DeactivateAbility()
    {
        if (activeAbility != AbilityType.None)
        {
            PlayerAbilities[activeAbility].OnDisable();
            activeAbility = AbilityType.None;
        }
    }
}

public enum AbilityType
{
    Hoverer,
    None
}
