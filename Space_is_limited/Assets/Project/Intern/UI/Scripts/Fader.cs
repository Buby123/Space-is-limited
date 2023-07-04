using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.UI
{
    /// <summary>
    /// Fader is a class that controls the fading animation
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Fader : Singleton<Fader>
    {
        private Animator _FadeAnimator;

        /// <summary>
        /// initializes the Animator
        /// </summary>
        private void Start()
        {
            _FadeAnimator = GetComponent<Animator>();
        }

        /// <summary>
        /// Starts the fading animation
        /// </summary>
        public void StartFade()
        {
            _FadeAnimator.ResetTrigger("FadeToBlack");
            _FadeAnimator.SetTrigger("FadeToBlack");
        }

        
    }
}