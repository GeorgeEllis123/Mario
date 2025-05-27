using UnityEngine;

public class MarioMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 10f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Brick Stuff")]
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private LayerMask powerUpLayer;
    [SerializeField] private float headCheckDistance = 0.1f;
    [SerializeField] private Transform headCheckLeft;
    [SerializeField] private Transform headCheckRight;

    [Header("Gumba Stuff")]
    [SerializeField] private float stompBounceForce = 12f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Transform stompCheck;
    [SerializeField] private float stompRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpBufferCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJumpInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
        ProcessBufferedJump();
        CheckHeadHit(headCheckLeft);
        CheckHeadHit(headCheckRight);
        CheckForStomp();
    }

    void HandleMovement()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float targetVelocityX = inputX * moveSpeed;
        float velocityDiff = targetVelocityX - rb.linearVelocity.x;
        float accelerationRate = (Mathf.Abs(targetVelocityX) > 0.01f) ? acceleration : deceleration;

        rb.linearVelocity += new Vector2(velocityDiff * accelerationRate * Time.fixedDeltaTime, 0f);
    }

    void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    void ProcessBufferedJump()
    {
        if (isGrounded && jumpBufferCounter > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCounter = 0f;
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
    }

    void CheckHeadHit(Transform headCheck)
    {
        RaycastHit2D brickHit = Physics2D.Raycast(headCheck.position, Vector2.up, headCheckDistance, brickLayer);
        if (brickHit.collider != null)
        {
            HandleBrickHit(brickHit.collider.gameObject);
        }

        RaycastHit2D powerUpHit = Physics2D.Raycast(headCheck.position, Vector2.up, headCheckDistance, powerUpLayer);
        if (powerUpHit.collider != null)
        {
            HandlePowerUpHit(powerUpHit.collider.gameObject);
        }
    }

    void HandleBrickHit(GameObject brick)
    {
        Destroy(brick, 0.1f);
    }

    void HandlePowerUpHit(GameObject powerUpBlock)
    {
        PowerUpBrick powerUpScript = powerUpBlock.GetComponent<PowerUpBrick>();
        if (powerUpScript != null && !powerUpScript.hasBeenHit)
        {
            powerUpScript.OnBrickHit();
        }
    }

    void CheckForStomp()
    {
        Collider2D hit = Physics2D.OverlapCircle(stompCheck.position, stompRadius, enemyLayer);
        if (hit != null)
        {
            if (rb.linearVelocity.y <= 0)
            {
                Destroy(hit.gameObject);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, stompBounceForce);
            }
        }
    }

}
