using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Start()
    {
        moveSpeed = 5f;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Bullet collided with: {collision.tag}");
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<HeartSystem>().TakeDamage(1);
            Destroy();
        }
        else if (collision.CompareTag("GEM"))
        {
            // If the bullet hits a GEM, ignore it.
            Debug.Log("Bullet hit a GEM, ignoring...");
        }
        else
        {
            // Log any other collisions that might be causing unexpected behavior.
            Debug.Log($"Bullet hit an unhandled object: {collision.name} with tag: {collision.tag}");
        }
    }

    private void Destroy()
    {
        gameObject.SetActive(false); // Deactivate the bullet.
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
