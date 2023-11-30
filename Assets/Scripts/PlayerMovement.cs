using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f; // Duration of dash in seconds.
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private bool isDashing;
    private float dashTimeLeft;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashTimeLeft = dashDuration;
        Vector2 dashDirection = movement.normalized; // Dash in the last movement direction.

        while (dashTimeLeft > 0f)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.deltaTime;
            yield return null; // Wait for the next frame.
        }

        rb.velocity = Vector2.zero; // Stops the dash by setting velocity to zero.
        isDashing = false;
    }
}
