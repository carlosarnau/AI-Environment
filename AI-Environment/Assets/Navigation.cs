using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        public GameObject target;
        public NavMeshAgent agent;
    }

    // Update is called once per frame
    void Update()
    {
        Seek();
    }

    void Seek()
    {
        agent.destination = target.transform.position;
    }
}