using UnityEngine;

public class MushroomMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    void FixedUpdate()
    {
        // Maintain horizontal movement
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
}
