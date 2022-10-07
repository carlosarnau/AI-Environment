using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.IO;

public class AIScripts : MonoBehaviour
{
    //Characters
    public NavMeshAgent agent;
    public GameObject target;

    //functions
    public bool seek;
    public bool wander;
    public bool hide;
    public bool patrol;
    public bool flee;

    //Stats
    public float freq = 0f;
    public float turnSpeed = 0.0f;
    public float movSpeed = 0.0f;
    public float acceleration = 0.1f;
    public float turnAcceleration = 0.1f;
    public float maxSpeed = 10.0f;
    public float maxTurnSpeed = 2.0f;
    Quaternion rotation;
    Vector3 movement;
    float stopDistance = 1.0f;


    // Update is called once per frame
    void Update()
    {

        if (wander) Wander();

    }


    void Seek()
    {

        agent.destination = target.transform.position;

    }


    void Wander()
    {
       /*
        float radius = 2f;
        float offset = 3f;

        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            Seek(hit.position);
        }
        else
        {
            worldTarget = transform.TrasnformPoint(-localTarget);
            worldTarget.y = 0f;
            if (NavMesh.SamplePosition(worldTarget, out hit, radius, NavMesh.AllAreas))
            {
                Seek(hit.position);
            }
        }
        */
    }


    void Hide()
    {



    }


    void Patrol()
    {



    }


    void Flee()
    {



    }
}
