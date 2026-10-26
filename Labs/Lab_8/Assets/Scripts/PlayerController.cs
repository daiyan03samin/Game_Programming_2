using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;     // Movement speed
    public float turnSpeed = 180f;   // Turning speed in degrees per second

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private float gravity = 9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input axes
        float horizontal = Input.GetAxis("Horizontal"); // For turning
        float vertical = Input.GetAxis("Vertical");     // For moving forward/backward

        // Rotate the character based on horizontal input
        transform.Rotate(0, horizontal * turnSpeed * Time.deltaTime, 0);

        // Move the character forward/backward
        if (controller.isGrounded)
        {
            // Set move direction forward
            moveDirection = transform.forward * vertical * moveSpeed;
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character controller
        controller.Move(moveDirection * Time.deltaTime);
    }
}
