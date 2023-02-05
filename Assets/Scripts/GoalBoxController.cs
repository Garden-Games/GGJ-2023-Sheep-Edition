using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalBoxController : MonoBehaviour
{

    public ParticleSystem winParticleSystem;
    public int DestroyWinCount = 5;
    public float GoalDistanceThreshold = 1.0f;

    public int destroyedCount = 0;

    public Animator gateAnimator;
    public GameObject goalSphere;
    public TextMeshPro sheepRemainingText;

    private bool isGoalComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        winParticleSystem.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoalComplete)
        {
            winParticleSystem.gameObject.SetActive(true);
        } else
        {
            sheepRemainingText.text = $"{destroyedCount} of {DestroyWinCount}";
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Flock>() != null)
        {
            // get the transform of the other
            GameObject otherGo = other.gameObject;
            Vector3 disp = transform.position - otherGo.transform.position;
            if (disp.magnitude < GoalDistanceThreshold) {
                // Destroy(other.gameObject);
                Flock f = otherGo.GetComponent<Flock>();
                f.SetGoalPos(transform.position);
                f.SetFlockingEnabled(false);
                destroyedCount += 1;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

    }

    private void OnDrawGizmos()
    {
        // draw a translucent red cube around the position of this objects boxcollider object
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, this.GetComponent<BoxCollider>().size);
        
    }
}
