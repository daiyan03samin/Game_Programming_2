using UnityEngine;

// Sends the player's movement to the Animator every frame
// so the right animation plays at the right time.
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;       // plays the animations
    private PlayerMovement movement; // our movement script
    private Rigidbody rb;            // to read the player's speed

    private void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Send the current speed to the blend tree
        animator.SetFloat("CharacterSpeed", rb.linearVelocity.magnitude);

        // On the ground? (controls Falling and ends the flip)
        bool grounded = movement.IsGrounded;
        animator.SetBool("IsGrounded", grounded);

        // When Fire1 (left mouse button / left Ctrl) is released, roll
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetTrigger("doRoll");
        }

        if (grounded)
        {
            // NEW: throw away a double jump request that was never used
            animator.ResetTrigger("doDoubleJump");
        }
        else
        {
            // NEW: Double jump. Space again in mid-air.
            // The flip only plays if PlayerMovement really jumped.
            if (Input.GetButtonDown("Jump") && movement.TryDoubleJump())
            {
                animator.SetTrigger("doDoubleJump");
            }
        }
    }
}
