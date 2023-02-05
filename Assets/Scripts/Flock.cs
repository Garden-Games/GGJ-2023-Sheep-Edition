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
    private bool isIdle = false;
    private float idleCycleTime = 0.0f;

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

    private Vector3 GetAvoidanceVector(Vector3 avoidPos, float desiredDistance) {
        Vector3 displacement = avoidPos - transform.position;
        return (-1 * displacement.normalized * (manager.desiredAvoidDistance - displacement.magnitude)) + transform.position;
    }

    private GameObject[] GetAvoidObjects() {
        GameObject[] avoidObjects = new GameObject[manager.avoidTags.Length];
        for (int i = 0; i < manager.avoidTags.Length; i++) {
            avoidObjects[i] = GameObject.FindGameObjectWithTag(manager.avoidTags[i]);
        }
        return avoidObjects;
    }

    private void UpdateGoalPos() {
        if (!manager) {
            manager = GameObject.Find("FlockManager").GetComponent<FlockManager>();
        }
        if (neighbors.Count > 0) {
            Vector3 newGoal = Vector3.zero;
            foreach (GameObject neighbor in neighbors) {
                if (neighbor != null) {
                    if (neighbor.GetComponent<Flock>().enabled) {
                        newGoal += neighbor.transform.position;
                    }
                }
            }
            internalGoalPos = newGoal / neighbors.Count;
            isIdle = false;
            
        }
        else {
            if (isIdle) {
                idleCycleTime += Time.deltaTime;
                if (idleCycleTime > manager.randomWalkUpdateFrequency) {
                    isIdle = false;
                    idleCycleTime = 0.0f;
                }
            }
            else {
                float rwd = manager.randomWalkDistance;
                float x = Random.Range(-rwd, rwd);
                float y = Random.Range(-rwd, rwd);
                float z = Random.Range(-rwd, rwd);
                internalGoalPos = transform.position + new Vector3(x, y, z);
                isIdle = true;
            }
        }

       // get nav mesh agent component of game object

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Vector3 goalPos = GetGoalPos();

        // check for objects we try to avoid

        GameObject[] avoidObjects = GetAvoidObjects();
        if (avoidObjects != null) {
            foreach(GameObject avoidObject in avoidObjects) {
                if (avoidObject != null) {
                    Vector3 displacement = avoidObject.transform.position - transform.position;
                    if (displacement.magnitude < manager.desiredAvoidDistance) {
                        goalPos = GetAvoidanceVector(avoidObject.transform.position, manager.desiredAvoidDistance);
                        isIdle = false;
                    }
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
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(pos);
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