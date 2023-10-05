using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player Controller:
/// - In-charge of controlling the following actions:
///   1. Move
///   2. Jump
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private bool isFacingRight = true;

    [Header("Move Settings:")]
    [SerializeField] private float movementSpeed;
    private float horizontalInput = 0.0f;

    [Header("Jump Settings:")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallingGravity;
    [SerializeField] private float castLength;
    [SerializeField] private LayerMask layersToHit;
    private float normalGravity;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        normalGravity = rb2D.gravityScale;
    }

    private void Update()
    {
        GetHorizontalInput();
        SetFacingDirection(horizontalInput);

        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb2D.velocity = new Vector2(horizontalInput * movementSpeed, rb2D.velocity.y);
    }

    private void Jump()
    {
        // INFO: Prevents the player from jumping in mid-air
        if (IsGrounded())
        {
            rb2D.gravityScale = normalGravity;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

                animator.SetBool("IsJumping", true);
            }
        }

        // INFO: Variable jump height made possible when 'space' key is
        // released prematurely whilst the player is still moving upwards
        if (Input.GetKeyUp(KeyCode.Space) && rb2D.velocity.y > 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
            rb2D.gravityScale = fallingGravity;

            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else if (rb2D.velocity.y < 0)
        {
            rb2D.gravityScale = fallingGravity;

            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
    }

    private void GetHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("MovementSpeed", Mathf.Abs(horizontalInput));
    }

    private bool IsGrounded()
    {
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsFalling", false);

        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0.0f, Vector2.down, castLength, layersToHit);
    }

    private void SetFacingDirection(float facingDirection)
    {
        // INFO: Changes the direction that the player is currently facing,
        // however it only does this once, so as to prevent assigning the same
        // values to it every time the function, as this is just unnecessary 
        if (!isFacingRight && facingDirection > 0)
        {
            isFacingRight = true;
            transform.localScale *= new Vector2(-1, 1);
        }
        else if (isFacingRight && facingDirection < 0)
        {
            isFacingRight = false;
            transform.localScale *= new Vector2(-1, 1);
        }
    }
}
