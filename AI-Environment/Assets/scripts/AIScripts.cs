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
    public bool hide;
    public bool flee;
    public bool wander;
    public bool patrol;
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


        if (seek) Seek();
        

    }



    void Seek()
    {
        agent.destination = target.transform.position;
    }
}
