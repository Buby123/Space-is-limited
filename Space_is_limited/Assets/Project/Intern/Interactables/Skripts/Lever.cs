using Project.InteractionHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Project.Interaction
{
    /// <summary>
    /// The Lever handles a interaction and send signals to other objects
    /// </summary>
    public class Lever : InteractionArea
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
        
        [Tooltip("ID of the signal")]
        [SerializeField] private int id;
        #endregion

        #region Triggers
        /// <summary>
        /// Set the startstate
        /// </summary>
        private void Awake()
        {
            InitAction(ToggleState, new List<UnityAction<bool>>() { HelpGraphic.SetActive });
            EventManager.Instance.PullLever(id, false);
        }

        #endregion

        #region VisualOutput
        /// <summary>
        /// Toggles the visual state of the lever to a different one
        /// </summary>
        private void ToggleState()
        {
            active = !active;

            if (active)
            {
                VisualLever.sprite = PushedLeverGraphic;
            }
            else
            {
                VisualLever.sprite = ReleasedLeverGraphic;
            }

            EventManager.Instance.PullLever(id, active);
        }
        #endregion
    }
}
