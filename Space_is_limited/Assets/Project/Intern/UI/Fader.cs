using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Fader : Singleton<Fader>
{
    private Animator FadeAnimator;

    private void Start()
    {
        FadeAnimator = GetComponent<Animator>();

        // Wait for Animation Trigger
    }

    public void StartFade()
    {
        FadeAnimator.ResetTrigger("FadeToBlack");
        FadeAnimator.SetTrigger("FadeToBlack");
    }

    
}
