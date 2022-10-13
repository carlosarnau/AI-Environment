using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    float speed;
    // Start is called before the first frame update
    void Start()
    {

        speed = Random.Range(FlockManager.FM.minSpeed, FlockManager.FM.maxSpeed);

    }


    // Update is called once per frame
    void Update()
    {

        ApplyRules();
        this.transform.Translate(0, 0, speed * Time.deltaTime);

    }


    void ApplyRules()
    {

        GameObject[] gos;
        gos = FlockManager.FM.allFish;
        Vector3 vcentre = new Vector3(121, 12, 105);
        Vector3 vavoid = new Vector3(121, 12, 105);
        float gSpeed = 0.01f;
        float nDistance;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {

            if (go != this.gameObject)
            {

                nDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (nDistance <= FlockManager.FM.neighbourDistance)
                {

                    vcentre = go.transform.position;
                    groupSize++;

                    if (nDistance < 1.0f)
                    {

                        vavoid = vavoid + (this.transform.position - go.transform.position);

                    }
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;

                }

            }

        }

        if (groupSize > 0)
        {

            vcentre = vcentre / groupSize;
            speed = gSpeed / groupSize;
            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), FlockManager.FM.rotationSpeed * Time.deltaTime);

            }

        }

    }

}
