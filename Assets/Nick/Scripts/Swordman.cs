using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Swordman : MonoBehaviour
{
    NavMeshAgent agent;

    public Transform player;

    public float range;
    public float attackRange;
    public float attackFreq;
    float distance;
    float attackDelay;

    public bool triggered;

    public RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= range && distance > attackRange)
        {
            agent.destination = player.position + -transform.forward * 1;
            triggered = true;
        }
        
        if (distance <= attackRange)
        {
            gameObject.transform.LookAt(player);
            attackDelay += Time.deltaTime * PlayerScript.gameSpeed;
            Debug.Log("WHAM!");

            if (attackDelay >= attackFreq + Random.Range(0f, 2f))
            {
                Debug.Log("KAWHAM!");
                if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
                {
                    Debug.Log("KABOOM!");
                    if (hit.transform.tag == "Player")
                    {
                        Debug.Log("SLAM!");
                        hit.transform.gameObject.GetComponent<PlayerScript>().hp -= 1;
                    }
                }
                attackDelay = 0;
            }
        }

        if (distance > range)
        {
            agent.destination = transform.position;
            triggered = false;
        }
        agent.speed = PlayerScript.gameSpeed * 3.5f;
    }
}
