using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class Formation : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    public Vector3 pos;
    public Quaternion rot;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Leader");
        transform.rotation = target.transform.rotation;
        transform.position = target.transform.TransformPoint(pos);
    }

    void Update()
    {
        agent.destination = target.transform.TransformPoint(pos);
    }
}