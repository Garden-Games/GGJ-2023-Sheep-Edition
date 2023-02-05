using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDogInMidAir : MonoBehaviour
{
    private bool isGrounded = false;
    private CharacterController ccColider;

    [Range(0.001f, 0.99999f)] public float slowDownMultiplier = 0.99f;

    private void OnEnable()
    {
        ccColider = gameObject.GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        if (!isGrounded)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            Vector3 rbVelocity =rb.velocity;
            rbVelocity.x *= slowDownMultiplier;
            rbVelocity.z *= slowDownMultiplier;
            rb.velocity = rbVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }


}
