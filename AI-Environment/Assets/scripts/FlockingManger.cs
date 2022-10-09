using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManger : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] allFish;
    int numFish;
    GameObject flockFish;
    float spawnRadius;
    public float minSpeed;
    public float maxSpeed;
    public float distanceNeighbour;
    public float rotationSpeed;
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; ++i)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnRadius, spawnRadius), 0, Random.Range(-spawnRadius, spawnRadius)); //Random Position (Circle)
            Vector3 pos = this.transform.position + randomPosition; 
            Vector3 randomize = new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)); // random vector direction
            
            allFish[i] = (GameObject)Instantiate(flockFish, pos,
                                Quaternion.LookRotation(randomize));
            
            allFish[i].GetComponent<FlockFish>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
