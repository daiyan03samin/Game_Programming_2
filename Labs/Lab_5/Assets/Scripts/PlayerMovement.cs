using UnityEngine;
using Cinemachine;

// Moves the player with a Rigidbody:
// A/D or Left/Right arrows to ROTATE, W/S or Up/Down to move Forward/Backward, Space to jump.
[RequireComponent(typeof(Rigidbody))] // Ensures that a Rigidbody component is attached to the GameObject
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 7f;      // units per second
    [SerializeField] private float turnSpeed = 150f;    // degrees per second
    [SerializeField] private float jumpForce = 5f;      // strength of the jump

    [Header("Ground Check Settings")]
    [SerializeField] private float groundDistance = 0.5f; // ray length
    [SerializeField] private LayerMask groundMask;        // ground layers

    private Rigidbody rb;               // the physics body we move
    private Vector3 moveDirection;      // worked out from input in Update()
    private float turnInput;            // horizontal input for rotation
    private bool isGrounded;            // true when standing on the ground
    private bool jumpRequested = false; // set in Update(), used later

    // Lets other scripts READ isGrounded, but not change it.
    // "=> isGrounded" means "when asked, return isGrounded".
    public bool IsGrounded => isGrounded;

    // The methods from the next steps go here, inside the class
    // Start runs once, just before the first frame
    private void Start()
    {
        // Find the Rigidbody on this GameObject so we can move it
        rb = GetComponent<Rigidbody>();

        // Freeze rotation so the player doesn't fall over when colliding
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        // 1. Ground Check
        isGrounded = Physics.Raycast(transform.position + transform.up * groundDistance / 2, -transform.up, groundDistance, groundMask);

        // 2. Read Inputs
        turnInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right turn input
        float moveZ = Input.GetAxisRaw("Vertical");  // W/S or Up/Down forward/backward

        // Rotate the player transform in Update for responsive turning visuals
        transform.Rotate(0f, turnInput * turnSpeed * Time.deltaTime, 0f);

        // Calculate forward movement relative to current facing direction
        moveDirection = transform.forward * moveZ;

        // 3. Handle Jump Input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }
    }

    private void MovePlayer()
    {
        // Calculate velocity based on current forward vector
        Vector3 targetVelocity = moveDirection * moveSpeed;

        // Apply movement while preserving vertical velocity (gravity/jumping)
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
    }

}