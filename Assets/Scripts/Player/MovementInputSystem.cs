using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputSystem : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpPower = 5f;
    public bool isFacingRight = false;
    public bool isGrounded = false;

    Rigidbody2D rb;
    Animator animator;
    float horizontalInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Event: Player/Move  → OnMove (Dynamic Context)
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // لو Move نوعه Vector2:
        horizontalInput = ctx.ReadValue<Vector2>().x;

        // لو خَليت Move محور واحد (1D Axis) بدّل للسطر التالي:
        // horizontalInput = ctx.ReadValue<float>();
    }

    // Event: Player/Jump  → OnJump (Dynamic Context)
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            var s = transform.localScale; s.x *= -1f; transform.localScale = s;
        }
    }

    void Update() { FlipSprite(); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }
}