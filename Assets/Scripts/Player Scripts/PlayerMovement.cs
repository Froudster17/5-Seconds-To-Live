using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private int maxJumps = 2, currentJumps = 2;
    [SerializeField] private float horizontal, speed = 8f, jumpingPower = 16f, dashingPower = 24f, dashingTime = 0.2f, dashingCooldown = 1f;
    [SerializeField] private bool isFacingRight = true, canDash = true, isDashing;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded() && rb.velocity.y <= 0)
        {
            currentJumps = maxJumps;
        }

        if (Input.GetButtonDown("Jump") && currentJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            currentJumps -= 1;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (horizontal != 0f && rb.velocity.y == 0)
        {
            animator.SetBool("Moving", true);
        } else
        {
            animator.SetBool("Moving", false);
        }

        if (rb.velocity.y != 0)
        {
            animator.SetBool("Jumping", true);
        } else
        {
            animator.SetBool("Jumping", false);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
