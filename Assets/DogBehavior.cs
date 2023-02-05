using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogBehavior : MonoBehaviour
{
    public float secondsBeforeAgent = 1.5f;
    private bool isGrounded = false;
    private bool hasStartedTimer = false;
    private NavMeshAgent agent;
    public static bool callDog = false;
    private bool isHeadedToPlayer = false;

    private void OnEnable()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        Physics.IgnoreLayerCollision(6, 7);
    }



    private void OnCollisionEnter(Collision collision)
    {
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
        if( isGrounded && !hasStartedTimer && gameObject.GetComponent<NavMeshAgent>().enabled == false)
        {
            hasStartedTimer = true;
            StartCoroutine(DogNavTimer());
        }

        if (callDog)
        {
            Vector3 playerPosition = GameObject.Find("Player").transform.position;
            agent.SetDestination(playerPosition);
            isHeadedToPlayer = true;
        }

    }


    private IEnumerator DogNavTimer()
    {
        yield return new WaitForSeconds(secondsBeforeAgent);
        agent.enabled = true;
        Vector3 initialAgentPosition = gameObject.transform.position;
        Physics.IgnoreLayerCollision(6, 7, false);
        agent.SetDestination(initialAgentPosition);
    }

    private void OnDisable()
    {
        agent.enabled = false;
        callDog = false;
        isHeadedToPlayer = false;
    }





}
