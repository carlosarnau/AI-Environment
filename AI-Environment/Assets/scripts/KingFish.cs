using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.AI;

public class KingFish : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] targets;
    public Vector3 pos;
    public Quaternion rot;

    
    int currentTarget = 0;
    int maxWaypoints = 9;
    void Start()
    {
        
        transform.rotation = targets[currentTarget].transform.rotation;
        //transform.position = target.transform.TransformPoint(pos);
        
    }

    void Update()
    {
        agent.destination = targets[currentTarget].transform.TransformPoint(pos);
        if (agent.remainingDistance < 2.0f)
        {
            NextTarget();
        }
    }

    void NextTarget()
    {
        if(currentTarget >= maxWaypoints)    currentTarget = 0;
        else currentTarget++;

    }

    
}
