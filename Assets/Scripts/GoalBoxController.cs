using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GoalBoxController : MonoBehaviour
{

    public ParticleSystem winParticleSystem;
    public int DestroyWinCount = 5;
    public float GoalDistanceThreshold = 20.0f;

    public int destroyedCount = 0;

    public Animator gateAnimator;
    public GameObject goalSphere;
    public TextMeshPro sheepRemainingText;

    private bool isGoalComplete = false;

    private bool winAnimationPlayed = false;

    private List<GameObject> triggeredSheep = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        winParticleSystem.gameObject.SetActive(false);
    }

    private IEnumerator SceneLoadCoroutine()
    {
        Debug.Log("Level Complete! Loading next level in 3 seconds...");
        yield return new WaitForSeconds(3);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex >= SceneManager.sceneCountInBuildSettings) {
            Debug.Log("You win the game!");
        }
        else {
            SceneManager.LoadScene(sceneIndex + 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> doneSheep = new List<GameObject>();
        foreach(GameObject sheep in triggeredSheep)
        {
            // get the transform of the other
            GameObject otherGo = sheep;
            Vector3 disp = transform.position - otherGo.transform.position;
            Debug.Log("Distance to goal: " + disp.magnitude);
            if (disp.magnitude < GoalDistanceThreshold) {
                // Destroy(other.gameObject);
                UnityEngine.AI.NavMeshAgent agent = sheep.GetComponent<UnityEngine.AI.NavMeshAgent>();
                agent.SetDestination(transform.position);
                sheep.GetComponent<Flock>().enabled = false;
                destroyedCount += 1;
                doneSheep.Add(otherGo);
                isGoalComplete = destroyedCount >= DestroyWinCount;
            }
        }
        foreach(GameObject sheep in doneSheep)
        {
            triggeredSheep.Remove(sheep);
        }
        if (isGoalComplete)
        {
            if (!winAnimationPlayed)
            {
                gateAnimator.Play("CloseGateDoors");
                goalSphere.SetActive(false);
                sheepRemainingText.text = "";
                winAnimationPlayed = true;
                StartCoroutine(SceneLoadCoroutine());
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            }
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
            triggeredSheep.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Flock>() != null)
        {
            triggeredSheep.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // draw a translucent red cube around the position of this objects boxcollider object
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, this.GetComponent<BoxCollider>().size);

        // draw a translucent green sphere with goaldistancethreshold radius at the position of this object
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f, 0.2f);
        Gizmos.DrawSphere(transform.position, GoalDistanceThreshold);
        
    }
}
