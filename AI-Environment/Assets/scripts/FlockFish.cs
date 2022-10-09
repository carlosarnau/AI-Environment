using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class FlockFish : MonoBehaviour
{
    public FlockingManger myManager;
    public Vector3 direction;

    public Vector3 cohesion = Vector3.zero;
    public Vector3 align = Vector3.zero;
    public Vector3 separation = Vector3.zero;
    public float speed;
    float angle;
    Vector3 center;
    float separationMult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cohesion = Vector3.zero;
        align = Vector3.zero;
        separation = Vector3.zero;
        

        int num = 0;
        //Debug.Log("me: " + this);
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);

                if (distance <= myManager.distanceNeighbour)
                {


                    cohesion += go.transform.position;

                    align += go.GetComponent<FlockFish>().direction;

                    separation -= (transform.position - go.transform.position) / (distance);
                    num++;
                }


            }

        }
        if (num > 0)
        {
            align /= num;


            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);

            cohesion = (cohesion / num - transform.position).normalized * speed;

            //point.transform.position = cohesion;

        }

        float randomAngle = Random.Range(-angle, angle);
        Vector3 randomVec = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));
        Vector3 vectorToCenter = center - transform.position;

        
        direction = (
            (cohesion + align + separation * separationMult).normalized +
            randomVec)
            * speed
            + vectorToCenter.normalized;

        //print("me: " + this + " cohesion:" + cohesion + " align: " + align + " separation: " + separation);
        //print("vector to center: " + vectorToCenter);

        transform.rotation = Quaternion.Slerp(transform.rotation,
                                          Quaternion.LookRotation(direction),
                                          myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }
}
