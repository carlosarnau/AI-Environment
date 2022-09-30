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
        if (Vector3.Distance(target.transform.position, transform.position) < stopDistance) return;

        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            Seek();
        }

        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);

        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxSpeed);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed * Time.deltaTime;
    }

    void Seek()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        movement = direction.normalized * acceleration;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }
}