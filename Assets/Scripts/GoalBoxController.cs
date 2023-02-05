using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBoxController : MonoBehaviour
{

    public ParticleSystem winParticleSystem;
    public int DestroyWinCount = 5;

    public int destroyedCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        winParticleSystem.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyedCount >= DestroyWinCount)
        {
            winParticleSystem.gameObject.SetActive(true);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Flock>() != null)
        {

            Destroy(other.gameObject);
            destroyedCount += 1;
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
