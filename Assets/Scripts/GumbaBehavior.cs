using UnityEngine;
using UnityEngine.SceneManagement;

public class GoombaController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private bool movesLeft;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float direction = movesLeft ? -1f : 1f;
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();
            if (playerRb != null && playerRb.linearVelocity.y < 0)
            {
                // mario does this stuff
                return;
            }

            // lose
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
