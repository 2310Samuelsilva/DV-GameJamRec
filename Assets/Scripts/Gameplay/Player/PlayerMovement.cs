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

    private WeaponState weaponState;
    private Animator animator;

    private Vector3 spawnPoint;
    [SerializeField] private float fallThresholdY = -10f;

    void Start()
    {
        weaponState = GetComponentInChildren<WeaponState>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        spawnPoint = transform.position;
    }

    void Update()
    {
        // Respawn if player falls too low
        if (transform.position.y < fallThresholdY)
        {
            Debug.Log("Respawn");
            Respawn();
        }

        if (weaponState.IsCharging())
        {
            StopMoving();
            return;
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            doJump = true;
        }
    }

    void StopMoving()
    {
        horizontalInput = 0;
        doJump = false;
    }

    void FixedUpdate()
    {
        isGrounded = IsGrounded();

        float moveX = moveSpeed * horizontalInput;

        rb.linearVelocity = new Vector3(moveX, rb.linearVelocity.y, 0);

        animator.SetFloat("speed", Mathf.Abs(horizontalInput));

        if (doJump && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, 0);
            animator.SetBool("isJumping", true);
        }

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            animator.SetBool("isJumping", false);
        }

        doJump = false;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // Respawn method
    private void Respawn()
    {
        transform.position = spawnPoint;


        rb.linearVelocity = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}