using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the animation of the player death
/// </summary>
[RequireComponent(typeof(Animator))]
public class DeathHandler : Singleton<DeathHandler>
{
    public UnityEvent OnDeathAnimationFinished;
    public UnityEvent OnDeathAnimationStart;
    
    private Animator _Fader;

    private void Start()
    {
        if (OnDeathAnimationFinished == null)
            OnDeathAnimationFinished = new UnityEvent();

        if (OnDeathAnimationStart == null)
            OnDeathAnimationStart = new UnityEvent();

        _Fader = GetComponent<Animator>();
    }

    public void TriggerDeath(UnityAction DeathAction)
    {
        OnDeathAnimationStart.Invoke();
        _Fader.SetTrigger("FadeToBlack");
        OnDeathAnimationFinished.AddListener(DeathAction);
    }

    public void DeathAnimationFinished()
    {
        OnDeathAnimationFinished.Invoke();
        OnDeathAnimationFinished.RemoveAllListeners();
    }
}
