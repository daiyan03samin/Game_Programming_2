using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float rotationY = 0.0f;
    float rotationX = 0.0f; // For vertical camera movement
    float verticalInput = 0.0f;
    float horizontalInput = 0.0f;
    public float speedValue = 50.0f;
    public float rotationSpeed = 10.0f;
    public float cameraSensitivity = 100.0f;
    public Transform cameraTransform; // Drag your camera object here in the Inspector
    public float maxLookAngle = 85f; // Limits camera vertical movement

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Confined; // Confines the cursor to the game window
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement input
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Camera rotation based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;

        rotationY += mouseX;
        rotationX -= mouseY; // Invert to make camera look up/down correctly
        rotationX = Mathf.Clamp(rotationX, -maxLookAngle, maxLookAngle); // Prevent the camera from looking too far up or down

        // Rotate player horizontally
        transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);

        // Apply vertical rotation to the camera (only rotate the camera, not the player)
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }


    void FixedUpdate()
    {
        // Movement logic
        Vector3 forwardMovement = verticalInput * transform.forward;
        Vector3 rightMovement = horizontalInput * transform.right;
        Vector3 direction = (forwardMovement + rightMovement).normalized;

        // Apply movement force
        rb.linearVelocity = (direction * speedValue);
    }
}
