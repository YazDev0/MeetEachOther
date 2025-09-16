using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInputSystem : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpPower = 7f;
    public bool isFacingRight = false;
    public bool isGrounded = false;


    public AudioClip jumpSound;
    public AudioClip footstepSound;
    public float footstepInterval = 0.3f;

    Rigidbody2D rb;
    Animator animator;
    public AudioSource audioSource;
    float horizontalInput;
    float footstepTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();


        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        horizontalInput = ctx.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        { 
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", true);


            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void Update()
    {
        FlipSprite();
        PlayFootstepSound();
    }

    void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            var s = transform.localScale; s.x *= -1f; transform.localScale = s;
        }
    }

    void PlayFootstepSound()
    {

        if (isGrounded && Mathf.Abs(horizontalInput) > 0.1f)
        {
            footstepTimer -= Time.deltaTime;
            if (footstepTimer <= 0f && footstepSound != null)
            {
                audioSource.PlayOneShot(footstepSound, 0.5f);
                footstepTimer = footstepInterval;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }


}

