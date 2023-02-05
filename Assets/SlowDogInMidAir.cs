using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDogInMidAir : MonoBehaviour
{

    private CharacterController ccColider;
    [Range(1f,10f)]public float slowDownForce = 1f;

    private void OnEnable()
    {
        ccColider = gameObject.GetComponent<CharacterController>();
    }

    public void FixedUpdate()
    {
        if (!ccColider.isGrounded)
        {
            gameObject.GetComponent<Rigidbody>().drag = slowDownForce;
        }

        else
        {
            gameObject.GetComponent<Rigidbody>().drag -= 0.1f;
        }
    }
}
