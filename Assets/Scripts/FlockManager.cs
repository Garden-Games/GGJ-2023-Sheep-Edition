using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    [Header("Flock Settings")]
    [Range(1.0f, 10.0f)] public float neighborhoodRadius = 1.7f;
    [Tooltip("How far the flock will try to stay from the player")]
    public float desiredAvoidDistance = 5.0f;
    // How much weight will be given to avoidance when an avoid object is nearby. Between 0.0 and 1.0
    [Range(0.0f, 1.0f)]
    public float avoidWeight = 0.3f;
    [Tooltip("When idle, how far away the sheep will try to walk")]
    public float randomWalkDistance = 5.0f;
    [Tooltip("How frequently the sheep will change their idle position")]
    public float randomWalkUpdateFrequency = 3.0f;
    [Tooltip("Tags that the flock members will try to avoid")]
    public string[] avoidTags = new string[] { "Player", "Dog" };

    [Header("Advanced Settings")]
    [Tooltip("How frequently the Flock component applies flock rules. Between 0.0 and 1.0")]
    [Range(0.0f, 1.0f)] public float flockUpdateFrequency = 0.01f;

    [Header("Debug")]
    [Tooltip("Hardcoded goal position, overrides other settings")]
    public Vector3 goalPos = Vector3.zero;
    [Tooltip("Hardcoded target to follow, overrides other settings including goal position")]
    public GameObject goalPosObject = null;
    public bool goalPosOverride = false;
}