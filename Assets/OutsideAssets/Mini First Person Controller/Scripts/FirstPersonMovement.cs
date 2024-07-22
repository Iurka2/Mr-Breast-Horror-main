using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;

public class FirstPersonMovement : MonoBehaviour {
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool isSlowWalking;
    public bool isMediumWalking;
    public bool isFullWalking;
    public float slowWalkingThreshold = 0.2f;
    public float mediumWalkinThreshold = 0.5f;
    public float fullWalkingThreshold = 1.2f;

    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake ( ) {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ( ) {
        // Update IsRunning from input.
        IsRunning = canRun && (TCKInput.GetAction("jumpBtn", EActionEvent.Press));

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if(speedOverrides.Count > 0) {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(TCKInput.GetAxis("Joystick", EAxisType.Horizontal) * targetMovingSpeed, (TCKInput.GetAxis("Joystick", EAxisType.Vertical) * targetMovingSpeed));


        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);

        isSlowWalking = rigidbody.velocity.magnitude > slowWalkingThreshold;
        isFullWalking = rigidbody.velocity.magnitude > fullWalkingThreshold;
        isMediumWalking = rigidbody.velocity.magnitude > mediumWalkinThreshold;
    }

}
