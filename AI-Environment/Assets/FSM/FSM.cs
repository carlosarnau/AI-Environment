using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour
{

    float offset = 5;
    float radius = 3;

    GameObject[] ABOCHI;
    public float distSitting = 10f;
    //public move_03 moves;
    UnityEngine.AI.NavMeshAgent agent;

    private WaitForSeconds wait = new WaitForSeconds(0.05f); // == 1/20
    delegate IEnumerator State();
    private State state;

    int timer = 0;
    int forcedWanderTimer = 0;
    GameObject foodPlants;

    IEnumerator Start()
    {

        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();

        ABOCHI = GameObject.FindGameObjectsWithTag("LookingSpots");
        // moves = gameObject.GetComponent<move_03>();


        yield return wait;

        state = Wander;

        while (enabled)
            yield return StartCoroutine(state());
    }

    private void Update()
    {

        foodPlants = ABOCHI[0];

        for (int i = 0; i < ABOCHI.Length; i++)
        {
            if (Vector3.Distance(foodPlants.transform.position, agent.transform.position) > Vector3.Distance(ABOCHI[i].transform.position, agent.transform.position))
            {
                foodPlants = ABOCHI[i];
            }// player to seatspot vs player to ABOCHI [i] --> take closer
        }




        if (state == Wander)
        {
            Wander();
        }
        else if (state == Approaching)
        {
            Approaching();
        }
        else if (state == Sittings)
        {
            Sittings();
        }




    }

    IEnumerator Wander()
    {
        Debug.Log("Wander state");
        if (forcedWanderTimer <= 0)
        {
            while (Vector3.Distance(agent.transform.position, foodPlants.transform.position) > distSitting)
            {



                Wander_();
                yield return wait;
            };

            state = Approaching;
        }
        else
        {

            while (forcedWanderTimer >= 0)
            {

                forcedWanderTimer--;

                Wander_();
                yield return wait;
            };

        }


    }

    IEnumerator Approaching()
    {
        Debug.Log("Approaching state");

        agent.speed = 2f;

        agent.destination = foodPlants.transform.position;

        bool sitting = false;
        while (Vector3.Distance(agent.transform.position, foodPlants.transform.position) < distSitting)
        {
            if (Vector3.Distance(foodPlants.transform.position, transform.position) < 0.5f)
            {
                sitting = true;
                break;
            };
            yield return wait;
        };

        if (sitting)
        {
            agent.speed = 0f;
            Debug.Log("Sitting");
            state = Sittings;
            timer = 12;
        }
        else
        {
            agent.speed = 1f;
            state = Wander;
        }
    }


    IEnumerator Sittings()
    {
        Debug.Log("Sittings");

        while (timer >= 0)
        {
            timer--;

            agent.speed = 0f;
            yield return wait;
        };

        state = Wander;
        forcedWanderTimer = 360;
        agent.speed = 3f;
    }


    void Wander_()
    {
        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
        localTarget += new Vector3(0, 0, offset);
        Vector3 worldTarget = transform.TransformPoint(localTarget);
        worldTarget.y = 0f;

        agent.destination = worldTarget;
    }
}
