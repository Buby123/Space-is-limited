using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Interactables
{
    /// <summary>
    /// The button handles interactions with doors and other objects
    /// </summary>
    public class Button : MonoBehaviour
    {
        #region Objects
        [Tooltip("Visual Output of the Button (Renderer)")]
        [SerializeField] private SpriteRenderer VisualButton;
        [Tooltip("Color when the Button is set to active")]
        [SerializeField] private Color ActiveColor;
        [Tooltip("Color when the Button is set to inactive")]
        [SerializeField] private Color DeactiveColor;
    
        private Vector3 _InitialPosition;
        private Rigidbody2D _RB;
        #endregion

        #region Variables
        [Tooltip("ID of the signal")]
        [SerializeField] private int id;
        [Tooltip("Speed with which the button flys back in position")]
        private float floatBackSpeed = 3f;
        [Tooltip("Position distance at which the button counts as pushed")]
        private float deltaPos = 0.1f;
        #endregion

        #region Init
        /// <summary>
        /// Sets the output to not pushed
        /// </summary>
        private void Awake()
        {
            ResetButton();
            _InitialPosition = transform.position;
            _RB = GetComponent<Rigidbody2D>();
        }
        #endregion

        #region Triggers

        /// <summary>
        /// Test if the button is pulled by the player and pushes it
        /// </summary>
        private void Update()
        {
            if (FloatToPosition())
            {
                PushButton();
            } else
            {
                ResetButton();
            }
        }
        #endregion

        #region VisualOutput
        /// <summary>
        /// Sets the visual state of the button to active
        /// </summary>
        private void PushButton()
        {
            VisualButton.color = ActiveColor;
            EventManager.Instance.PushButton(id);
        }

        /// <summary>
        /// Sets the visual state of the button to inactive
        /// </summary>
        private void ResetButton()
        {
            VisualButton.color = DeactiveColor;
        }

        /// <summary>
        /// Floats back to the position, where the button was set
        /// </summary>
        /// <returns>Returns true if button is more the deltaPos away from start</returns>
        private bool FloatToPosition()
        {
            var distance = _InitialPosition.y - transform.position.y;
            _RB.velocity = new Vector2(0, distance * floatBackSpeed);
            return deltaPos < distance;
        }
        #endregion
    }
}
