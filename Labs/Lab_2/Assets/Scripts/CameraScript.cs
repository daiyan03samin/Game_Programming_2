using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [Header("Mouse Look")]
    public float mouseSensitivity = 0.1f;
    public float lookLimit = 80.0f;

    private float verticalLook = 0.0f;
    private Vector2 lookInput;

    // Called automatically by PlayerInput component via "Send Messages"
    // OR bound programmatically via C# Input Actions
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    public void OnCancel(InputValue value)
    {
        if (value.isPressed)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Calculate pitch (up/down) for the camera
        float mouseY = lookInput.y * mouseSensitivity;
        verticalLook -= mouseY;
        verticalLook = Mathf.Clamp(verticalLook, -lookLimit, lookLimit);

        transform.localRotation = Quaternion.Euler(verticalLook, 0f, 0f);
    }
}