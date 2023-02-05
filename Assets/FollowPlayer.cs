using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    [Range(0.001f, 0.5f)]
    public float followSpeed;

    private Vector3 interpolatedPosition;
    
    void Start()
    {
        interpolatedPosition = player.position;
    }

    void Update()
    {
        interpolatedPosition = interpolatedPosition * (1 - followSpeed) + player.position * followSpeed;
        transform.position = interpolatedPosition + offset;
    }
}
