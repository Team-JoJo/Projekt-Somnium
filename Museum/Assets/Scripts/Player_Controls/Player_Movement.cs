using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;
    public float maxVelocityChange;
    public float jumpHeight;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = false;
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        if (isGrounded && Input.GetButton("Jump"))
        {
            rb.velocity = new Vector3(velocity.x, jumpHeight, velocity.z);
            isGrounded = false;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
