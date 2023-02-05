using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : MonoBehaviour
{
    public float secondsBeforeAgent = 1.5f;

    bool isGrounded = false;

    private void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);

        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        print(collision.gameObject.name);

        if (collision.gameObject.tag.Equals("Ground"))
        {
            isGrounded = false;
        }
    }


    private void Update()
    {
        if( isGrounded && gameObject.GetComponent<NavMeshAgent>().enabled == false)
        {
            StartCoroutine(DogNavTimer());
        }
        
    }


    private IEnumerator DogNavTimer()
    {
        yield return new WaitForSeconds(secondsBeforeAgent);
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
       
    }





}
