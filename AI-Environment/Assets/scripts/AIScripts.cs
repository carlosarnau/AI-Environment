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
    float radius = 10.0f;
    float offset = 10.0f;

    void Start()
    {

        if (wander) Wander();

    }


    // Update is called once per frame
    void Update()
    {

        if (wander)
        {
            if (agent.remainingDistance < 10.0f )
            Wander();
        }

        /*
        if () 
        {
            Vector3 targetDir = target.transform.position - transform.position;
            float lookAhead = targetDir.magnitude / agent.speed;
            Seek(target.transform.position + target.transform.forward * lookAhead);
        }
        */

    }


    void Seek(Vector3 pos)
    {

        agent.destination = pos;

    }


    void Wander()
    {

        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);

        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        Seek(worldTarget);

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
