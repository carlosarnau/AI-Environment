using UnityEngine;

public class AIScripts : MonoBehaviour
{
    public bool seek;

    public GameObject target;
    
    float freq = 0f;
    Quaternion rotation;
    Vector3 movement;
    float acceleration;
    float turnSpeed = 1.0f;
    float turnAcceleration = 0.2f;
    float maxTurnSpeed = 1.0f;
    float movSpeed = 3.0f;
    float maxSpeed = 10.0f;
    float stopDistance = 2.0f;
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <
        stopDistance) return;
        Seek();   // calls to this function should be reduced
        turnSpeed += turnAcceleration * Time.deltaTime;
        turnSpeed = Mathf.Min(turnSpeed, maxTurnSpeed);
        movSpeed += acceleration * Time.deltaTime;
        movSpeed = Mathf.Min(movSpeed, maxSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              rotation, Time.deltaTime * turnSpeed);
        transform.position += transform.forward.normalized * movSpeed *
                              Time.deltaTime;
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
