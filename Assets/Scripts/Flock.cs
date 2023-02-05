using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Flock : MonoBehaviour {

    float speed;

    private FlockManager manager;
    private Vector3 fixedGoalPosition = Vector3.zero;
    private bool navigateToFixedGoal = false;
    private bool flockingEnabled = true;

    public SphereCollider flockNeighborhoodCollider;



    // private list of gameobjects called neighbors
    private List<GameObject> neighbors = new List<GameObject>();
    private Vector3 internalGoalPos = Vector3.zero;



    void Start() {
        if (!manager) {
            manager = GameObject.Find("FlockManager").GetComponent<FlockManager>();
        }



    }


    void Update() {
        // if random number between 0 and 1 is less than manager.flockUpdateFrequency
        if (!manager) {
            manager = GameObject.Find("FlockManager").GetComponent<FlockManager>();
        }

        // Set the sphere collider radius to the flock neighborhood radius
        flockNeighborhoodCollider.radius = manager.neighborhoodRadius;

        if (flockingEnabled && Random.Range(0.0f, 1.0f) < manager.flockUpdateFrequency) {
            UpdateGoalPos();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<Flock>() != null) {
            neighbors.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Flock>() != null) {
            neighbors.Remove(other.gameObject);
        }
    }


    private GameObject[] GetNeighbors() {

        // return empty array of gameobjects
        return new GameObject[0];
    }

    public Vector3 CalculateNeighborhoodCenter() {

        return Vector3.zero;
    }

    private void UpdateGoalPos() {
        if (!manager) {
            manager = GameObject.Find("FlockManager").GetComponent<FlockManager>();
        }
        if (neighbors.Count > 0) {
            Vector3 newGoal = Vector3.zero;
            foreach (GameObject neighbor in neighbors) {
                if (neighbor != null) {
                    newGoal += neighbor.transform.position;
                }
            }
            internalGoalPos = newGoal / neighbors.Count;
        }


        // get nav mesh agent component of game object

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 goalPos = GetGoalPos();

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects != null) {
            foreach(GameObject playerObject in playerObjects) {
                Vector3 playerDisplacement = playerObject.transform.position - transform.position;
                if (playerDisplacement.magnitude < manager.desiredPlayerDistance) {
                    goalPos = goalPos - (2 * playerDisplacement);
                }
            }
        }
        agent.SetDestination(goalPos);
    }

    public Vector3 GetGoalPos() {
        if (!manager) {
            manager = GameObject.Find("FlockManager").GetComponent<FlockManager>();
        }
        if (manager.goalPosOverride) {
            if (manager.goalPosObject != null) {
                return manager.goalPosObject.transform.position;
            }
            else {
                return manager.goalPos;
            }
        }
        else {
            if (navigateToFixedGoal) {
                return fixedGoalPosition;
            }
            else {
                return internalGoalPos;
            }
        }
    }

    public void SetGoalPos(Vector3 pos) {
        fixedGoalPosition = pos;
        navigateToFixedGoal = true;
    }

    public void SetFlockingEnabled(bool enabled) {
        flockingEnabled = enabled;
    }

    public void ClearGoalPos() {
        navigateToFixedGoal = false;
    }

    private void OnDrawGizmos() {
        // draw a green arrow to the goal
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
        Gizmos.DrawRay(transform.position, GetGoalPos() - transform.position);

        // draw a red translucent sphere at the goal 
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawSphere(GetGoalPos(), 0.5f);


        // draw a yellow translucent sphere with radius manager.neighborhoodRadius
        Gizmos.color = new Color(1.0f, 1.0f, 0.0f, 0.02f);
        Gizmos.DrawSphere(transform.position, manager.neighborhoodRadius);

        
    }
}