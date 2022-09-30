using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seeking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        public GameObject target;

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
    }

    // Update is called once per frame
    void Update()
    {
        Wander();
    }

    void Wander()
    {
        float radius = 2f;
        float offset = 3f;

        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(worldTarget, out NavMeshHit hit, radius, NavMesh.AllAreas))
        {
            Seek(hit.psoition);
        }
        else
        {
            worldTarget = transform.TrasnformPoint(-localTarget);
            worldTarget.y = 0f;
            if (NavMesh.SamplePosition(worldTarget, out hit, radius, NavMesh.AllAreas))
            {
                Seek(hit.psoition);
            }
        }
    }
}