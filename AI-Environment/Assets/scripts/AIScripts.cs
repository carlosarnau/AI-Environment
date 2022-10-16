using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.IO;
using System.Linq;
using System;

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
    public float minDistance = 20.0f;
    float stopDistance = 1.0f;
    float radius = 10.0f;
    float offset = 10.0f;
    public GameObject[] hidingSpots;
    public GameObject[] waypoints;
    int patrolWP = 0;


    void Start()
    {

        if (wander) Wander();

    }


    // Update is called once per frame
    void Update()
    {
        if(seek)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if ((distance < minDistance))
            {

                Seek(target.transform.position);

            }
        }
        

        else if (wander)
        {

            if (agent.remainingDistance < 10.0f)
                Wander();
            else Hide();

        }

        else if (patrol)
        {

            if (!agent.pathPending && agent.remainingDistance < 0.5f) Patrol();

        }

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

        hidingSpots = GameObject.FindGameObjectsWithTag("hide");

        Func<GameObject, float> distance = (hs) => Vector3.Distance(target.transform.position, hs.transform.position);
        GameObject hidingSpot = hidingSpots.Select(ho => (distance(ho), ho)).Min().Item2;
        Vector3 dir = hidingSpot.transform.position - target.transform.position;

        Ray backRay = new Ray(hidingSpot.transform.position, -dir.normalized);
        RaycastHit info;

        hidingSpot.GetComponent<Collider>().Raycast(backRay, out info, 50f);
        Seek(info.point + dir.normalized);

    }


    void Patrol()
    {

        patrolWP = (patrolWP + 1) % waypoints.Length;
        Seek(waypoints[patrolWP].transform.position);

    }


    void Flee()
    {



    }

}
