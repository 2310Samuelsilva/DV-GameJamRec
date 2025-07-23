using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 10.0f;

    public bool isGrounded = false;
    private bool doJump = false;
    private Rigidbody rb;
    private float horizontalInput;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            doJump = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = IsGrounded();

        // Calculate horizontal movement
        float moveX = moveSpeed * horizontalInput;

        // Move the player
        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, 0);

        // Set Speed parameter for Idle/Run animation
        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        // Jumping
        if (doJump && isGrounded)
        {   
            Debug.Log("Jumping!");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            animator.SetBool("isJumping", true); // trigger jump animation
        }

        // Falling faster
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Update isJumping animation based on grounded check
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("isJumping", false); // land
        }

        doJump = false;
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Optional: Draw the sphere in the editor
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}