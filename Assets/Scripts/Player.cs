using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

/*    [SerializeField] private GameInput gameInput;*/
    private float moveSpeed = 2f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float playerHeight;
    public LayerMask whatIsGround;
    public float groundDrag;
    bool grounded;

    Vector3 moveDir;

    Rigidbody rbPlayer;


    private void Start ( ) {
        rbPlayer = GetComponent<Rigidbody>();

    }

    private void Update ( ) {
        MyInput();
        SpeedControl();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);


        //handle drag
        if (grounded)
        {
            rbPlayer.drag = groundDrag;
        } else {
            rbPlayer.drag = 0;
        }
    }

    private void FixedUpdate ( ) {
        MovePlayer();
    }

    private void MyInput ( ) {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }

    private void MovePlayer () {
        moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rbPlayer.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl ( ) {

        Vector3 flatVel = new Vector3(rbPlayer.velocity.x, 0f, rbPlayer.velocity.z);

        if (flatVel.magnitude > moveSpeed) {

            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rbPlayer.velocity = new Vector3(limitedVel.x, rbPlayer.velocity.y, limitedVel.z);
        }
    }

 
}
