using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{

    [Header("Sheep Spawner Settings")]
    public GameObject creaturePrefab;
    public int numCreature = 20;
    public Vector3 spawnLimits = new Vector3(5.0f, 0.0f, 5.0f);

    public GameObject flockManager;


    private GameObject[] allCreatures;
    // Start is called before the first frame update
    void Start()
    {
        allCreatures = new GameObject[numCreature];

        for (int i = 0; i < numCreature; ++i) {

            Vector3 pos = this.transform.position + new Vector3(
                Random.Range(-spawnLimits.x, spawnLimits.x),
                Random.Range(-spawnLimits.y, spawnLimits.y),
                Random.Range(-spawnLimits.z, spawnLimits.z));

            allCreatures[i] = Instantiate(creaturePrefab, pos, Quaternion.identity);
            
            // cast allCreatures[i] into object of type Flock
        }

        if (flockManager != null) {
            flockManager.GetComponent<FlockManager>().SetAllCreatures(allCreatures);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
