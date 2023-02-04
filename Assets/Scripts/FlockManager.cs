using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    [Header("Flock Manager Behavior")]
    [Tooltip("Automatically adjust the flock goal position over time")]
    public bool autoAdjustGoalPos = false;
    [Tooltip("Apply movement limits set by moveLimits to the flock")]
    public bool applyMovementLimits = false;
    [Tooltip("The limits of the area the flock can move in. X, Y, Z")]
    public Vector3 moveLimits = new Vector3(5.0f, 0.0f, 5.0f);

    [Header("Creature Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed = 2.5f;
    [Range(0.0f, 50.0f)] public float maxSpeed = 2.5f;
    [Range(1.0f, 10.0f)] public float neighbourDistance = 5.0f;
    [Range(1.0f, 5.0f)] public float rotationSpeed = 2.5f;

    [Header("Flock Status")]
    [SerializeField] private Vector3 goalPos = Vector3.zero;
    [SerializeField] private GameObject[] allCreatures;

    [Header("Advanced Settings")]
    [Tooltip("How frequently the FlockManager updates the flock's direction. Between 0.0 and 1.0")]
    [Range(0.0f, 1.0f)] public float flockManagerDirectionUpdateFrequency = 0.1f;
    [Tooltip("How frequently the Flock component applies flock rules. Between 0.0 and 1.0")]
    [Range(0.0f, 1.0f)] public float flockUpdateFrequency = 0.1f;

    [Header("Debug")]
    [SerializeField] private float gizmoYOffset = 0.50f;
    [SerializeField] private float gizmoSphereRadius = 1.0f;

    void Start() {
    }


    void Update() {
        // get a random number between 0 and 1
        float random = Random.Range(0.0f, 1.0f);
        // if the random number is less than the update frequency
        if (autoAdjustGoalPos && random < flockManagerDirectionUpdateFrequency) {
            // update the goal position
            goalPos = this.transform.position + new Vector3(
                Random.Range(-moveLimits.x, moveLimits.x),
                Random.Range(-moveLimits.y, moveLimits.y),
                Random.Range(-moveLimits.z, moveLimits.z));
        }
    }

    private void OnDrawGizmos() {
        // set the gizmo color to a translucent blue
        Gizmos.color = new Color(0.0f, 1.0f, 1.0f, 0.5f);
        Vector3 originPos = new Vector3(this.transform.position.x, this.transform.position.y + gizmoYOffset, this.transform.position.z);
        Gizmos.DrawSphere(originPos, gizmoSphereRadius);

        if (applyMovementLimits) {
            // set the gizmo color to a translucent blue
            Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.5f);
            // draw a cube with bounds defined by the originPos and the moveLimits
            Vector3 adjustedOriginPos = new Vector3(originPos.x - (moveLimits.x), originPos.y - moveLimits.y, originPos.z - (moveLimits.z));
            Gizmos.DrawCube(adjustedOriginPos, moveLimits * 2.0f);
        }

        // set gizmo color to a translucent red
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Vector3 gizmoPos = new Vector3(goalPos.x, goalPos.y + gizmoYOffset, goalPos.z);
        Gizmos.DrawSphere(gizmoPos, gizmoSphereRadius);
    }

    public Vector3 GetGoalPos() {
        return goalPos;
    }

    public void SetGoalPos(Vector3 newGoalPos) {
        goalPos = newGoalPos;
    }

    public GameObject[] GetAllCreatures() {
        return allCreatures;
    }

    public void SetAllCreatures(GameObject[] creatures) {
        allCreatures = creatures;
        // loop through all creatures and set their flock manager
        foreach (GameObject creature in allCreatures) {
            creature.GetComponent<Flock>().SetFlockManager(this);
        }
    }

    public float GetFlockUpdateFrequency() {
        return flockUpdateFrequency;
    }
}