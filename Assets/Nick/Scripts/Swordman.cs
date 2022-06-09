using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swordman : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform player;

    public float range;
    float distance;

    public bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= range)
        {
            agent.destination = player.position + -transform.forward * 1;
            triggered = true;
        }
        else
        {
            agent.destination = transform.position;
            triggered = false;
        }
    }
}
