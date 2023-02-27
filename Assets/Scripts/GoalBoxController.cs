using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoalBoxController : MonoBehaviour
{
    [Header("Loading Next Level Settings")]
    public Image fadeScreen;
    public float fadeTime = 3f;

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
        FadeScreen.FadeIn(fadeScreen,fadeTime);
    }

    private IEnumerator SceneLoadCoroutine()
    {
        Debug.Log("Level Complete! Loading next level in 3 seconds...");
        
        yield return new WaitForSeconds(3);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex + 1 >= SceneManager.sceneCountInBuildSettings) {
            Debug.Log("You win the game!");
            SceneManager.LoadScene(0);
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
            if (!isGoalComplete) {
                Flock flock = sheep.GetComponent<Flock>();
                flock.SetGoalPos(transform.position);
                flock.enabled = false;
                destroyedCount += 1;
                sheep.GetComponent<AudioSource>().Play();
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
                gameObject.tag = "CompletedGoalBox";
                gateAnimator.Play("CloseGateDoors");
                gateAnimator.gameObject.GetComponent<AudioSource>().Play();
                goalSphere.SetActive(false);
                sheepRemainingText.text = "";
                winAnimationPlayed = true;
                GameObject[] goalBoxes = GameObject.FindGameObjectsWithTag("GoalBox");
                bool allComplete = true;
                foreach(GameObject goalBox in goalBoxes)
                {
                    if (!goalBox.GetComponent<GoalBoxController>().isGoalComplete)
                    {
                        allComplete = false;
                    }
                }
                if (allComplete) {
                    FadeScreen.FadeOut(fadeScreen, fadeTime);
                    StartCoroutine(SceneLoadCoroutine());
                }
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

