using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f; 
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField]
    private AudioSource audioSource;

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
        Vector2 dashDirection = movement.normalized; 

        while (dashTimeLeft > 0f)
        {
            rb.velocity = dashDirection * dashSpeed;
            dashTimeLeft -= Time.deltaTime;
            yield return null; 
        }
        audioSource.Play();
        rb.velocity = Vector2.zero; 
        isDashing = false;
    }
}
