using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    /// <summary>
    /// Controls the Movement of the player
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : Singleton<PlayerController>
    {
        #region objects
        private Rigidbody2D _Controller;
        #endregion

        #region variables
        [Tooltip("Jumpforce")]
        [SerializeField] float jumpForce = 20f;
        [Tooltip("Toggles the force to move")]
        [SerializeField] float sideSpeed = 10f;
        [Tooltip("Toggles the maximum speed the player can reach")]
        [SerializeField] float maxSideAcceleration = 7f;
        [Tooltip("Toggles the look direction of the player")]
        [SerializeField] bool flippedLeft = true;
        [SerializeField] GameObject Appearance;
        [SerializeField] Animator Animator;

        private bool _jump;
        private float _moveSidewardsInput;
        #endregion

        #region Propertys
        [field: SerializeField] public Collider2D PlayerCollider { get; private set; }
        public bool Active { get; set; } = true;
        public bool FlippedLeft => flippedLeft;

        public Rigidbody2D Controller { get => _Controller; set => _Controller = value; }
        #endregion

        #region UnityFunctions
        /// <summary>
        /// Initializes the Rigidbodys
        /// </summary>
        private void Start()
        {
            Controller = GetComponent<Rigidbody2D>();
            PlayerInput.Instance.OnJump.AddListener(OnJump);
            PlayerInput.Instance.OnDown.AddListener(OnFalltrough);
            PlayerInput.Instance.OnSidewardValue.AddListener(OnSidewardValue);
        }
        #endregion

        #region Input Functions
        /// <summary>
        /// Initiates a Jump if player is on ground
        /// </summary>
        public void OnJump(bool jump)
        {
            if (!Active)
            {
                return;
            }

            this._jump = jump;

            if (jump && GroundChecker.Instance.onGround)
            {
                Controller.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// Moves the player sideways and flips the image if needed
        /// </summary>
        /// <param name="moveSidewardsInput">float from -1 to 1, that characterizes left to right</param>
        public void OnSidewardValue(float moveSidewardsInput)
        {
            if (!Active)
            {
                return;
            }

            this._moveSidewardsInput = moveSidewardsInput;

            if (moveSidewardsInput > 0 && flippedLeft)
            {
                Flip();
            }
            else if (moveSidewardsInput < 0 && !flippedLeft)
            {
                Flip();
            }
        }

        /// <summary>
        /// Lets the player fall through platforms by changing it's LayerMask
        /// </summary>
        public void OnFalltrough(bool pushed)
        {
            if (!Active)
            {
                return;
            }

            if (pushed)
            {
                Appearance.layer = LayerMask.NameToLayer("PlayerOffPlatform");
            }
            else
            {
                Appearance.layer = LayerMask.NameToLayer("Player");
            }
        }

        /// <summary>
        /// In this FixedUpdate the players sidewards movement is applied via a Force.
        /// 
        /// Also the speed of falling is increased if the jump key is not pressed anymore.
        /// </summary>
        private void FixedUpdate()
        {
            if (!Active)
            {
                return;
            }

            // Move sidewards
            Controller.AddForce(Vector2.right * HelpFunctions.GetAccelerationVelocity(_moveSidewardsInput * sideSpeed, Controller.velocity.x, maxSideAcceleration), ForceMode2D.Force);

            // Fall fast
            if (!_jump && Controller.velocity.y > 0)
            {
                Controller.velocity = new Vector2(Controller.velocity.x, Controller.velocity.y * 0.8f);
            }
        }

        /// <summary>
        /// Update the status of the player Animation
        /// </summary>
        private void Update()
        {
            Animator.SetFloat("XVelocity", Mathf.Abs(Controller.velocity.x));
            Animator.SetFloat("YVelocity", Controller.velocity.y);
            Animator.SetBool("OnGround", GroundChecker.Instance.onGround);
            Animator.SetBool("UsingAbility", !Active);
        }
        #endregion

        #region OurFunctions
        /// <summary>
        /// Flips the Player per localScale parameter
        /// </summary>
        private void Flip()
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            flippedLeft = !flippedLeft;
        }
        #endregion
    }
}