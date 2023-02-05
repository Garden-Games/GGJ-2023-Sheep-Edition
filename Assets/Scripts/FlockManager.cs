using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    [Header("Flock Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed = 2.5f;
    [Range(0.0f, 50.0f)] public float maxSpeed = 2.5f;
    [Range(1.0f, 5.0f)] public float rotationSpeed = 2.5f;
    [Range(1.0f, 10.0f)] public float neighborhoodRadius = 2.0f;
    [Tooltip("How far the flock will try to stay from the player")]
    public float desiredPlayerDistance = 5.0f;
    [Tooltip("When idle, how far away the sheep will try to talk")]
    public float randomWalkDistance = 5.0f;
    [Tooltip("How frequently the sheep will change their idle position")]
    public float randomWalkUpdateFrequency = 3.0f;

    [Header("Advanced Settings")]
    [Tooltip("How frequently the Flock component applies flock rules. Between 0.0 and 1.0")]
    [Range(0.0f, 1.0f)] public float flockUpdateFrequency = 0.01f;

    [Header("Debug")]
    [Tooltip("Hardcoded goal position, overrides other settings")]
    public Vector3 goalPos = Vector3.zero;
    [Tooltip("Hardcoded target to follow, overrides other settings including goal position")]
    public GameObject goalPosObject = null;
    public bool goalPosOverride = false;


    [Header("Deprecated")]
    [Tooltip("moveLimits is deprecated don't use it")]
    public Vector3 moveLimits = new Vector3(5.0f, 5.0f, 5.0f);
    [Tooltip("flag to applyMoveLimits")]
    public bool applyMovementLimits = false;
}