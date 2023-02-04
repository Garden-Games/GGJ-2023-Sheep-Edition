using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    float speed;
    bool turning = false;

    private FlockManager manager;



    void Start() {
        
    }

    public void SetFlockManager(FlockManager manager) {
        this.manager = manager;
        speed = Random.Range(manager.minSpeed, manager.maxSpeed);
        
    }


    void Update() {
        if (!manager) {
            return;
        }
        Bounds b = new Bounds(manager.transform.position, manager.moveLimits * 2.0f);

        if (manager.applyMovementLimits && !b.Contains(transform.position)) {

            turning = true;
        } else {

            turning = false;
        }

        if (turning) {

            Vector3 direction = manager.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                manager.rotationSpeed * Time.deltaTime);
        } else {
            float updateThreshold = manager.GetFlockUpdateFrequency();
            if (Random.Range(0.0f, 1.0f) <= updateThreshold) {

                speed = Random.Range(manager.minSpeed, manager.maxSpeed);
            }


            if (Random.Range(0.0f, 1.0f) <= updateThreshold) {
                ApplyRules();
            }
        }

        this.transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    private void ApplyRules() {

        GameObject[] gos;
        gos = manager.GetAllCreatures();

        Vector3 vCentre = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;

        float gSpeed = 0.01f;
        float mDistance;
        int groupSize = 0;

        foreach (GameObject go in gos) {

            if (go != this.gameObject) {

                mDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (mDistance <= manager.neighbourDistance) {

                    vCentre += go.transform.position;
                    groupSize++;

                    if (mDistance < 1.0f) {

                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0) {

            vCentre = vCentre / groupSize + (manager.GetGoalPos() - this.transform.position);
            speed = gSpeed / groupSize;

            if (speed > manager.maxSpeed) {

                speed = manager.maxSpeed;
            }

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if (direction != Vector3.zero) {

                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(direction),
                    manager.rotationSpeed * Time.deltaTime);
            }
        }
    }
}