using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // Physics-related operations should go here
    {
        // Get input for horizontal and vertical axes
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the player's forward and right vectors
        Vector3 movement = (transform.right * moveX) + (transform.forward * moveZ);

        // Apply movement to the rigidbody
        rb.AddForce(movement * moveSpeed, ForceMode.Acceleration); // ForceMode.Force
    }
}
