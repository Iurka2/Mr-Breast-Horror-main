using UnityEngine;

public class HeadJump : MonoBehaviour {
    public float jumpForce = 10f;  // Force applied for the jump
    private Rigidbody rb;         // Reference to the Rigidbody component

    void Start ( ) {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

  
    }

    public void Jump ( ) {
        // Apply an upward force to the Rigidbody to make the head jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
