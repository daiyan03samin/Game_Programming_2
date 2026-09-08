using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6.0f;
    public float jumpForce = 5.0f;

    [Header("Ground Check")]
    public LayerMask groundMask;
    public float groundCheckDistance = 0.2f;

    Rigidbody rb;
    public float mouseSensitivity = 2.0f;

    private bool isGrounded;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // don't let collisions tip the player over
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        // Yaw: turn the whole player left/right
        transform.Rotate(0f, mouseX, 0f, Space.World);

        // Ground Check using a Raycast downwards
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundMask);

        // Capture jump input in Update so frames aren't missed
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Move relative to facing direction, flattened to the ground
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 targetVelocity = (forward * v + right * h) * moveSpeed;

        // Apply Jump or keep current vertical velocity
        if (jumpRequested)
        {
            targetVelocity.y = jumpForce;
            jumpRequested = false;
        }
        else
        {
            targetVelocity.y = rb.linearVelocity.y; // keep gravity untouched
        }

        rb.linearVelocity = targetVelocity;
    }
}