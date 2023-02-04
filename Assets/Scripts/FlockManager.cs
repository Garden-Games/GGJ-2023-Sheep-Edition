using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour {

    public static FlockManager FM;
    public GameObject creaturePrefab;
    public int numCreature = 20;
    public Vector3 moveLimits = new Vector3(5.0f, 5.0f, 5.0f);
    [Tooltip("How frequently the FlockManager updates the flock's direction. Between 0.0 and 1.0")]
    public float normalizedUpdateFrequency = 0.1f;

    [Header("Creature Settings")]
    [Range(0.0f, 5.0f)] public float minSpeed = 2.5f;
    [Range(0.0f, 5.0f)] public float maxSpeed = 2.5f;
    [Range(1.0f, 10.0f)] public float neighbourDistance = 5.0f;
    [Range(1.0f, 5.0f)] public float rotationSpeed = 2.5f;
    
    [SerializeField] private Vector3 goalPos = Vector3.zero;
    [SerializeField] private GameObject[] allCreatures;

    void Start() {

        allCreatures = new GameObject[numCreature];

        for (int i = 0; i < numCreature; ++i) {

            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-moveLimits.x, moveLimits.x),
                Random.Range(-moveLimits.y, moveLimits.y),
                Random.Range(-moveLimits.z, moveLimits.z));

            allCreatures[i] = Instantiate(creaturePrefab, pos, Quaternion.identity);
        }

        FM = this;
        goalPos = this.transform.position;
    }


    void Update() {
        // get a random number between 0 and 1
        float random = Random.Range(0.0f, 1.0f);
        // if the random number is less than the update frequency
        if (random < normalizedUpdateFrequency) {
            // update the goal position
            goalPos = this.transform.position + new Vector3(
                Random.Range(-moveLimits.x, moveLimits.x),
                Random.Range(-moveLimits.y, moveLimits.y),
                Random.Range(-moveLimits.z, moveLimits.z));
        }
    }

    public Vector3 GetGoalPos() {
        return goalPos;
    }

    public GameObject[] GetAllCreatures() {
        return allCreatures;
    }
}