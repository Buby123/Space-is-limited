using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.InteractionHelpers;

namespace Project.Interactables
{
    /// <summary>
    /// Moves the Box if the player stands above it
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(IsTouched))]
    public class BoxWallRecoverer : MonoBehaviour
    {
        #region Variables
        private Transform _PlayerTF;
        private Rigidbody2D _RB;
        private IsTouched _IsTouched;
        
        [Tooltip("Force of footsteps from player above the box")]
        [SerializeField] private float movement_force = 10f;
        #endregion

        #region UnityMethods
        /// <summary>
        /// Inits the parameters
        /// </summary>
        private void Start()
        {
            _PlayerTF = GameObject.FindGameObjectWithTag("Player").transform;
            _RB = GetComponent<Rigidbody2D>();
            _IsTouched = GetComponent<IsTouched>();
        }

        /// <summary>
        /// Moves the box when player stands above
        /// </summary>
        private void Update()
        {
            if (_IsTouched.State && PlayerAboveBox())
            {
                MoveBoxOnPlayerMovement();
            }
        }
        #endregion

        #region Movebox back on movement
        /// <summary>
        /// Test if the player is higher than the box
        /// </summary>
        /// <returns></returns>
        private bool PlayerAboveBox()
        {
            var BoxPosition = transform.position;
            return _PlayerTF.position.y > BoxPosition.y;
        }

        /// <summary>
        /// Move box in opposite direction of player movement
        /// </summary>
        private void MoveBoxOnPlayerMovement()
        {
            var VerticalVector = Input.GetAxis("Horizontal");
            _RB.AddForce(new Vector2(movement_force * -VerticalVector, 0));
        }
        #endregion
    }
}

