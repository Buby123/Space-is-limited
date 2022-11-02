using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private Rigidbody2D Controller;
    [SerializeField] LayerMask GroundLayer;

    [SerializeField] float speed = 0f;
    [SerializeField] float jumpSpeed = 0f;
    [SerializeField] float playerHeight = 1f;
    [SerializeField] float maxVelocity = 100f;

    private void Awake()
    {
        Controller = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 velocity = Controller.velocity;
        float inputVector = Input.GetAxis("Horizontal");
        bool jump = false;

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("TryJump");
            if (IsGrounded())
            {
                jump = true;
                Debug.Log("Jump");
            }
        }

        Controller.AddForce(new Vector2(inputVector * speed, jump ? jumpSpeed : 0f) * Time.deltaTime);

        Controller.velocity = new Vector2(Mathf.Clamp(Controller.velocity.x, -maxVelocity, maxVelocity),
                              Controller.velocity.y);

        /*
        if (velocity.sqrMagnitude > sqrtSpeed)
        {
            Controller.velocity = velocity.normalized * maxSpeed;
        }*/
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, playerHeight, GroundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
