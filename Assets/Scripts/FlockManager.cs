using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    [Header("Flock Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed = 2.5f;
    [Range(0.0f, 50.0f)] public float maxSpeed = 2.5f;
    [Range(1.0f, 5.0f)] public float rotationSpeed = 2.5f;
    [Range(1.0f, 10.0f)] public float neighborhoodRadius = 5.0f;

    [Header("Advanced Settings")]
    [Tooltip("How frequently the Flock component applies flock rules. Between 0.0 and 1.0")]
    [Range(0.0f, 1.0f)] public float flockUpdateFrequency = 0.01f;

    [Header("Debug")]
    [Tooltip("Hardcoded goal position, overrides other settings")]
    public Vector3 goalPos = Vector3.zero;
    public bool goalPosOverride = false;

    [Header("Deprecated")]
    [Tooltip("moveLimits is deprecated don't use it")]
    public Vector3 moveLimits = new Vector3(5.0f, 5.0f, 5.0f);
    [Tooltip("flag to applyMoveLimits")]
    public bool applyMovementLimits = false;
}